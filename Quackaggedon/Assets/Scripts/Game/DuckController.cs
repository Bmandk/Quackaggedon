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
        
        private SpriteRenderer _spriteRenderer;
        
        private bool _isHovered = false;
        private bool _isSelected = false;


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
        }
        
        public void Deselect()
        {
            _isSelected = false;
        }
    }
}