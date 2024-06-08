using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightDirtOnHover : MonoBehaviour
{
    public Material hoverMat;
    public Material defaultMat;

    public Vector3 targetPoint; // The point to yeet objects to
    public float speed = 50f; // Speed at which objects are yeeted

    public SpriteRenderer[] spritesToChange;

    private bool hovering;
    private bool areYeeting = false;

    public GameObject[] allInteractabls;

    public GameObject collBlocker;

    private void Update()
    {
        if (hovering && Input.GetMouseButtonDown(0))
        {
            areYeeting = true;
            AudioController.Instance.PlayTrashSlosh();
            AudioController.Instance.PlayTrashSwoosh();
        }

        if (areYeeting)
        {
            for (int i = spritesToChange.Length - 1; i >= 0; i--)
            {
                if (spritesToChange[i] != null)
                {
                    // Move the object towards the target point
                    this.transform.position = Vector3.MoveTowards(spritesToChange[i].transform.position, targetPoint, speed * Time.deltaTime);
                    spritesToChange[i].transform.position = Vector3.MoveTowards(spritesToChange[i].transform.position, targetPoint, speed * Time.deltaTime);

                    // Check if the object has reached the target point
                    if (Vector3.Distance(spritesToChange[i].transform.position, targetPoint) < 0.1f)
                    {
                        Destroy(spritesToChange[i]);
                        // Remove the destroyed object from the array
                        spritesToChange[i] = null;
                    }
                }
            }
        }

        if (AreSpritesDEstroyed())
        {
            Destroy(collBlocker.gameObject);
            Destroy(this.gameObject);
        }
    }

    private bool AreSpritesDEstroyed()
    {
        foreach (var sprite in spritesToChange)
        {
            if (sprite != null)
            {
                return false;
            }
        }
        return true;
    }

    private void OnMouseEnter()
    {
        ToolTipController.toolTipInfo = "Remove Trash";
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
