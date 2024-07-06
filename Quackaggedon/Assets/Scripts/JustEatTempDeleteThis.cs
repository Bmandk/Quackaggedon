using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustEatTempDeleteThis : MonoBehaviour
{
    public Animator animator;
    public void EatTemp()
    {
        animator.SetBool("Eat", true);
    }
}
