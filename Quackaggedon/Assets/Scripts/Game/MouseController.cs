using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace DuckClicker
{
    public class MouseController : MonoBehaviour
    {
        private DuckSelector _hoveredDuck;
        private DuckSelector _selectedDuck;
        public static List<DuckSelector> _selectedDucks = new List<DuckSelector>();

        public static bool selectingDucks; //Miro: 🤮🤮🤮 hahahah trying your type of comments Jonathan!

        private void Update()
        {
            if (ButtonBlocker.IsBlocked)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * 1000f, 1000, LayerMask.GetMask("DuckSelector"));

            if (selectingDucks)
            {
                SelectDucks(hit);
            }
            else
            {
                if (!References.Instance.menuController.IsBlockingUiOpen())
                    FeedDucksOnClick(hit);
            }
        }

        private void FeedDucksOnClick(RaycastHit2D hit)
        {
            bool hitDuck = hit.collider != null && hit.collider.CompareTag("DuckSelector");
            if (hitDuck)
            {
                DuckSelector duckSelector = hit.collider.GetComponentInParent<DuckSelector>();
                if (Input.GetMouseButtonDown(0) && duckSelector != null)
                {
                    References.Instance.menuController.PulseQuackStats();
                    PlayerFoodStats.AddTimesDuckClicked(duckSelector.transform.gameObject.GetComponent<DuckData>().duckType,1);
                    duckSelector.Feed();
                }
            }
        }


        private bool SelectDucks(RaycastHit2D hit)
        {
            bool hitDuck = hit.collider != null && hit.collider.CompareTag("DuckSelector");
            if (hitDuck)
            {
                DuckSelector duckSelector = hit.collider.GetComponentInParent<DuckSelector>();
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (_selectedDucks.Contains(duckSelector))
                    {
                        duckSelector.Deselect();
                        _selectedDucks.Remove(duckSelector);
                        _selectedDuck = null;
                    }
                    else if (duckSelector != null)
                    {
                        _selectedDuck = duckSelector;
                        _selectedDuck.Select();
                        _selectedDucks.Add(_selectedDuck);
                    }
                }
                else
                {
                    if (duckSelector != _hoveredDuck)
                    {
                        if (_hoveredDuck != null)
                            _hoveredDuck.Unhover();                        
                        _hoveredDuck = duckSelector;
                        duckSelector.Hover();
                    }
                    else if (duckSelector == null)
                    {
                        duckSelector.Unhover();
                        _hoveredDuck = null;
                    }
                }

            }
            else
            {
                if (_hoveredDuck != null && !_selectedDucks.Contains(_selectedDuck))
                {
                    _hoveredDuck.Unhover();
                    _hoveredDuck = null;
                }                           
            }
            return hitDuck;
        }

        public List<DuckSelector> GetAllSelectedDucks()
        {
            return _selectedDucks;
        }

        public void DeselectAllDucks()
        {
            foreach (var duck in _selectedDucks)
            {
                duck.Deselect();
            }
            _selectedDucks.Clear();
        }

        public void ResetAllSelectedDucks()
        {
            _selectedDucks.Clear();
        }
    }
}