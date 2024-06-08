using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondDirtColliders : MonoBehaviour
{
    public Material hoverMat;
    public Material defaultMat;
    public string toolTip;

    public Animator animator;
    public Animator animatorBroom;

    public SpriteRenderer[] spritesToChange;

    private bool hovering;

    private bool startedClean = false;

    private void Update()
    {
        if (hovering && Input.GetMouseButtonDown(0) && !startedClean)
        {
            //startedClean! && 
            startedClean = true;
            ToolTipController.toolTipInfo = "";
            ToolTipController.showToolTip = false;
            animator.SetTrigger("Clean");
            animatorBroom.SetTrigger("Broom");
        }
    }

    private void OnMouseEnter()
    {
        ToolTipController.toolTipInfo = toolTip;
        ToolTipController.showToolTip = true;

        hovering = true;
        foreach (var sprite in spritesToChange)
        {
            if (sprite != null)
                sprite.material = hoverMat;
        }
    }

    private void OnMouseExit()
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;

        hovering = false;
        foreach (var sprite in spritesToChange)
        {
            if (sprite != null)
                sprite.material = defaultMat;
        }
    }
}
