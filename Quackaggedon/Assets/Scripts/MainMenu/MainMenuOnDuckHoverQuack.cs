using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuOnDuckHoverQuack : MonoBehaviour
{
    public Animator animator;

    private void OnMouseEnter()
    {
        if (!MainMenuController.Instance.creditsMenu.activeSelf)
            animator.SetTrigger("Quack");
    }
}
