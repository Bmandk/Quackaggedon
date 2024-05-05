using System;
using System.Collections;
using UnityEngine;

namespace DuckClicker
{
    public class DuckController : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve startGrowingCurve;
        [SerializeField]
        private float targetScale = 1.0f;
        
        [SerializeField]
        private Color _hoverColor = Color.red;
        [SerializeField]
        private Color _selectColor = Color.green;
        private Color _defaultColor = Color.white; 
        
        private SpriteRenderer _spriteRenderer;
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
        
        private bool _isHovered = false;
        private bool _isSelected = false;
        
        [SerializeField]
        private float _currencyBase = 1.0f;
        [SerializeField]
        private float _timeMultiplier = 1.0f;
        
        private float _calculatedCurrencyPerSecond = 0.0f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.material.GetColor(OutlineColor);
        }

        void Start()
        {
            StartCoroutine(GrowDuck());
            
            UpdateCurrency();
        }
        
        public float CalculateCurrency()
        {
            return _currencyBase * _timeMultiplier;
        }

        private void UpdateCurrency()
        {
            float currency = CalculateCurrency();
            
            if (currency != _calculatedCurrencyPerSecond)
            {
                CurrencyController.AddCurrencyPerSecond(currency - _calculatedCurrencyPerSecond);
                _calculatedCurrencyPerSecond = currency;
            }
        }

        public IEnumerator GrowDuck()
        {
            float time = 0.0f;

            while (time < startGrowingCurve.keys[startGrowingCurve.length - 1].time)
            {
                time += Time.deltaTime;
                float scale = startGrowingCurve.Evaluate(time) * targetScale;
                transform.localScale = new Vector3(scale, scale, 1.0f);
                yield return null;
            }
        }

        public void Hover()
        {
            if (_isSelected)
            {
                return;
            }
            
            _spriteRenderer.material.SetColor(OutlineColor, _hoverColor);
            _isHovered = true;
        }
        
        public void Unhover()
        {
            if (_isSelected)
            {
                return;
            }
            
            _spriteRenderer.material.SetColor(OutlineColor, _defaultColor);
            _isHovered = false;
        }
        
        public void Select()
        {
            _spriteRenderer.material.SetColor(OutlineColor, _selectColor);
            _isSelected = true;
            CurrencyController.AddCurrency(_currencyBase);
        }
        
        public void Deselect()
        {
            if (_isHovered)
            {
                _spriteRenderer.material.SetColor(OutlineColor, _hoverColor);
            }
            else
            {
                _spriteRenderer.material.SetColor(OutlineColor, _defaultColor);
            }
            _spriteRenderer.material.SetColor(OutlineColor, _defaultColor);
            _isSelected = false;
        }
    }
}