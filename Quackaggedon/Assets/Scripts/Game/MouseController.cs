using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace DuckClicker
{
    public class MouseController : MonoBehaviour
    {
        private DuckController _hoveredDuck;
        private DuckController _selectedDuck;
        
        private void Update()
        {
            if (ButtonBlocker.IsBlocked)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * 1000f);
            if (hit.collider == null)
            {
                Debug.Log("No collider hit");
                return;
            }
            // Only allow feeding if we're not hovering over a duck
            CheckDuck(hit);
            bool hitBackplane = hit.collider.CompareTag("Backplane");
            if (hitBackplane)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DuckFeeder.SelectedFeeder.ToggleFeeding(true);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                DuckFeeder.SelectedFeeder.ToggleFeeding(false);
            }
        }

        private bool CheckDuck(RaycastHit2D hit)
        {
            bool hitDuck = hit.collider.CompareTag("Duck");
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