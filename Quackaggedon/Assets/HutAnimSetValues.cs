using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutAnimSetValues : MonoBehaviour
{
    public Animator hutAnim;

    private void Start()
    {
        if (HutRevealController.revealedLvl1)
        {
            SetGroundToCleaned();
            SetLibraryToShown();
            SetPillowToShown();
        }

        if (HutRevealController.revealedLvl2)
        {
            SetWallToCleaned();
            SetTVToShown();
        }

        if (HutRevealController.revealedLvl3)
        {
            SetRoofToCleaned();
            SetMagicalCircleToShown();
            SetShroomsToShown();
        }
    }

    public void PlayMiniPop()
    {
        AudioController.Instance.PlayMiniPop();
    }

    public void CleanRoof()
    {
        hutAnim.SetTrigger("Clean_Roof");
    }

    public void SetRoofToCleaned()
    {
        hutAnim.SetBool("Cleaned_Roof",true);
    }

    public void CleanWall()
    {
        hutAnim.SetTrigger("Clean_Wall");
    }

    public void SetWallToCleaned()
    {
        hutAnim.SetBool("Cleaned_Wall", true);
    }
    public void CleanGround()
    {
        hutAnim.SetTrigger("Clean_Ground");
    }

    public void SetGroundToCleaned() 
    {
        hutAnim.SetBool("Cleaned_Ground", true);
    }

    public void ShowLights()
    {
        hutAnim.SetTrigger("Reveal_Lights");
    }


    public void SetLightsToShown()
    {
        hutAnim.SetBool("Revealed_Lights", true);
    }

    public void ShowShrooms()
    {
        hutAnim.SetTrigger("Reveal_Shrooms");
    }


    public void SetShroomsToShown()
    {
        hutAnim.SetBool("Revealed_Shrooms", true);
    }

    public void ShowLibrary()
    {
        hutAnim.SetTrigger("Reveal_Reader");
    }


    public void SetLibraryToShown()
    {
        hutAnim.SetBool("Revealed_Reader", true);
    }

    // /\

    public void ShowTV()
    {
        hutAnim.SetTrigger("Reveal_TV");
    }

    public void SetTVToShown()
    {
        hutAnim.SetBool("Revealed_TV", true);
    }

    public void ShowPillow()
    {
        hutAnim.SetTrigger("Reveal_Pillow");
    }

    public void SetPillowToShown()
    {
        hutAnim.SetBool("Revealed_Pillow", true);
    }

    public void ShowMagicCircle()
    {
        hutAnim.SetTrigger("Reveal_Magic");
    }

    public void SetMagicalCircleToShown()
    {
        hutAnim.SetBool("Revealed_Magic", true);
    }
}
