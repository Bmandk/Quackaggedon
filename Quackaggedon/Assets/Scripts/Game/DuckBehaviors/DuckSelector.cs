using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DuckSelector : MonoBehaviour
{
    [SerializeField]
    private Material _defaultMat;
    
    [SerializeField]
    private Material _highlightMat;
    
    [SerializeField]
    private Material _selectMat;
    
    [SerializeField]
    private SpriteRenderer[] _duckSprites;

    private bool isSelected;
    public void Select()
    {
        isSelected = true;
        ChangeDuckMaterial(_selectMat);
    }
    
    public void Deselect()
    {
        isSelected = false;
        ChangeDuckMaterial(_defaultMat);
    }
    
    public void Hover()
    {
        ChangeDuckMaterial(_highlightMat);
    }
    
    public void Unhover()
    {
        if (!isSelected)
            ChangeDuckMaterial(_defaultMat);
    }

    private void ChangeDuckMaterial(Material newMaterial)
    {
        foreach (var sprite in _duckSprites)
        {
            sprite.material = newMaterial;
        }   
    }
}