using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfButtonSetter : MonoBehaviour
{
    public bool isContinueButton;
    public Animator anim;

    private void OnEnable()
    {
        if (isContinueButton)
            anim.SetBool("FadeInButton", true);
        else
            anim.SetBool("FadeInButton", false);
    }
}
