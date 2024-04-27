using System;
using UnityEngine;

namespace DuckClicker
{
    public class MouseController : MonoBehaviour
    {
        private DuckController _hoveredDuck;
        private DuckController _selectedDuck;
        
        private DuckFeeder _feeder;
        
        private void Awake()
        {
            _feeder = FindObjectOfType<DuckFeeder>();
        }
        
        private void Update()
        {
            // Only allow feeding if we're not hovering over a duck
            bool hitDuck = CheckDuck();
            if (!hitDuck)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _feeder.ToggleFeeding(true);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _feeder.ToggleFeeding(false);
                }
            }
        }

        private bool CheckDuck()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            bool hitDuck = hit.collider != null && hit.collider.CompareTag("Duck");
            if (hitDuck)
            {
                if (hit.collider.CompareTag("Duck"))
                {
                    DuckController duck = hit.collider.GetComponentInParent<DuckController>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_selectedDuck != null)
                        {
                            _selectedDuck.Deselect();
                        }
                        _selectedDuck = duck;
                        _selectedDuck.Select();
                    }
                    else
                    {
                        if (_hoveredDuck != duck)
                        {
                            if (_hoveredDuck != null)
                            {
                                _hoveredDuck.Unhover();
                            }
                            _hoveredDuck = duck;
                            _hoveredDuck.Hover();
                        }
                    }
                }
                else
                {
                    if (_hoveredDuck != null)
                    {
                        _hoveredDuck.Unhover();
                        _hoveredDuck = null;
                    }
                }
            }
            else
            {
                if (_hoveredDuck != null)
                {
                    _hoveredDuck.Unhover();
                    _hoveredDuck = null;
                }
            }
            
            return hitDuck;
        }
    }
}