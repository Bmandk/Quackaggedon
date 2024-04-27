using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool IsBlocked { get; private set; }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsBlocked = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsBlocked = false;
    }
}