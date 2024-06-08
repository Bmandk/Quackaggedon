using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFoodToCookingChef : MonoBehaviour
{
    public GameObject ingredientPrefab;

    public void StartMovingIngredient(Vector3 endPos, FoodType foodType)
    {
        StartCoroutine(MoveIngredient(endPos, foodType));
    }

    private IEnumerator MoveIngredient(Vector3 endPos, FoodType foodType)
    {
        Vector3 startPos = this.transform.position;
        // Instantiate the prefab under the parent transform
        GameObject ingredientInstance = Instantiate(ingredientPrefab, startPos, Quaternion.identity);

        ingredientInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = References.Instance.GetFoodData(foodType).foodIconRevealed;

        // Initialize the time variables
        float elapsedTime = 0f;
        float duration = 0.4f;

        // Loop until the elapsed time exceeds the duration
        while (elapsedTime < duration)
        {
            if (ingredientInstance != null)
                ingredientInstance.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);

            // Increment the elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is set to the end position
        if (ingredientInstance != null)
            ingredientInstance.transform.position = endPos;

        // Destroy the instance
        if (ingredientInstance != null)
            Destroy(ingredientInstance);
    }
}
