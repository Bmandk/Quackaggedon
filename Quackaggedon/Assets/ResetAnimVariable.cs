using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimVariable : MonoBehaviour
{
    public Animator animator;
    public string boolToReset;


    public void SetBoolToFalse()
    {
        animator.SetBool(boolToReset, false);   
    }
}
