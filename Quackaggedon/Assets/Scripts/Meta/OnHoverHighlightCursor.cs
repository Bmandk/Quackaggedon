using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverHighlightCursor : MonoBehaviour
{
    public Texture2D cursorTextureDefault;
    public Texture2D cursorTextureHighlight;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTextureHighlight, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}

