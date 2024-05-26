using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIconFollowMouse : MonoBehaviour
{
    public Canvas parentCanvas;
    public GameObject cursorImage;

    private void Start()
    {
        cursorImage.SetActive(true);    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        transform.position = parentCanvas.transform.TransformPoint(movePos);
    }
}
