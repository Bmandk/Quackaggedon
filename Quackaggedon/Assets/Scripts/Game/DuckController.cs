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
        }

        void Start()
        {
            
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

        public void Hover()
        {
            if (_isSelected)
            {
                return;
            }
            
            _isHovered = true;
        }
        
        public void Unhover()
        {
            if (_isSelected)
            {
                return;
            }
            
            _isHovered = false;
        }
        
        public void Select()
        {
            _isSelected = true;
            CurrencyController.AddCurrency(_currencyBase);
        }
        
        public void Deselect()
        {
            _isSelected = false;
        }
    }
}