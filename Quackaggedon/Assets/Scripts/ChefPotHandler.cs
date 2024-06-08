using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChefPotHandler : MonoBehaviour
{
    public GameObject chefPotUi; 

    public Image[] allFoodIcons;

    public GameObject littleFood;
    public GameObject mediumFood;
    public GameObject muchFood;

    public enum Amount
    {
        LittleFood,
        MediumFood,
        MuchFood,
    }

    public void ShowChefPot(FoodType foodType, Amount level, Vector3 chefWorldLocation)
    {
        var screenPos = References.Instance.mainCam.WorldToScreenPoint(chefWorldLocation);

        foreach (var food in allFoodIcons)
        {
            food.sprite = References.Instance.GetFoodData(foodType).foodIconRevealed;
        }

        littleFood.SetActive(false);
        mediumFood.SetActive(false);
        muchFood.SetActive(false);

        switch (level)
        {
            case Amount.LittleFood:
                littleFood.SetActive(true);
                break;
            case Amount.MediumFood:
                mediumFood.SetActive(true);
                break;
            case Amount.MuchFood:
                muchFood.SetActive(true);   
                break;
            default:
                break;
        }

        chefPotUi.transform.position = screenPos;
        chefPotUi.SetActive(true);
    }
}
