using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTypeOrCont : MonoBehaviour
{
    public GameObject[] textObjs;
    public void ContinueType()
    {
        foreach (var t in textObjs)
        {
            if (t.activeSelf && !t.GetComponent<TextAnimator_TMP>().allLettersShown)
            {
                GetComponent<TypewriterByCharacter>().SkipTypewriter();
            }
            else
            {

            }
        }
    }        
}
