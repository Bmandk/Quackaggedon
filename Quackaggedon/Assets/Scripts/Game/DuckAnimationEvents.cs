using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DuckAnimationEvents : MonoBehaviour
{
    public void PlayRandomQuack()
    {
        AudioController.Instance.PlayRandomQuack();
    }

    public SpriteRenderer[] duckImages;
    public void MakeDuckShowOutsideMask()
    {
        foreach (var duckImage in duckImages)
        {
            duckImage.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }

    public void MakeDuckIgnoreMask()
    {
        foreach (var duckImage in duckImages)
        {
            duckImage.maskInteraction = SpriteMaskInteraction.None;
        }
    }
}
