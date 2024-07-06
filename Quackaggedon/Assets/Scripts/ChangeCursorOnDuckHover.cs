using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorOnDuckHover : MonoBehaviour
{
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        Cursor.SetCursor(References.Instance.hoverDuckCursor, hotSpot, cursorMode);
    }

    void OnMouseDown()
    {
        Cursor.SetCursor(References.Instance.clickDuckCursor, hotSpot, cursorMode);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(References.Instance.hoverDuckCursor, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
