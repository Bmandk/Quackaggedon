using System;
using System.Collections;
using UnityEngine;

namespace DuckClicker
{
    public class DuckController : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve startGrowingCurve;

        private bool _isSelected = false;


        public void Hover()
        {
            if (_isSelected)
            {
                return;
            }
        }

        public void Unhover()
        {
            if (_isSelected)
            {
                return;
            }
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