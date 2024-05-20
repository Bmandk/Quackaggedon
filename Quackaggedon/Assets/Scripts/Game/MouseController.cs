using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace DuckClicker
{
    public class MouseController : MonoBehaviour
    {
        private DuckSelector _hoveredDuck;
        private DuckSelector _selectedDuck;
        
        private void Update()
        {
            if (ButtonBlocker.IsBlocked)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * 1000f);
            // Only allow feeding if we're not hovering over a duck
            bool hitDuck = CheckDuck(hit);
            if (!hitDuck)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DuckFeeder.SelectedFeeder.PerformFeedingHandAnimation();
                }
            }
        }

        private bool CheckDuck(RaycastHit2D hit)
        {
            bool hitDuck = hit.collider != null && hit.collider.CompareTag("Duck");
            if (hitDuck)
            {
                DuckSimple duck = hit.collider.GetComponentInParent<DuckSimple>();
                DuckSelector duckSelector = hit.collider.GetComponentInParent<DuckSelector>();
                if (Input.GetMouseButtonDown(0))
                {
                    if (_selectedDuck != null)
                    {
                        _selectedDuck.Deselect();
                    }
                    _selectedDuck = duckSelector;
                    _selectedDuck.Select();
                    
                    duck.OnClick();
                }
                else
                {
                    if (_hoveredDuck != duckSelector && _selectedDuck != duckSelector)
                    {
                        if (_hoveredDuck != null)
                        {
                            _hoveredDuck.Unhover();
                        }
                        _hoveredDuck = duckSelector;
                        _hoveredDuck.Hover();
                    }
                }
            }
            else
            {
                if (_hoveredDuck != null && _hoveredDuck != _selectedDuck)
                {
                    _hoveredDuck.Unhover();
                    _hoveredDuck = null;
                }
            }
            
            return hitDuck;
        }
    }
}