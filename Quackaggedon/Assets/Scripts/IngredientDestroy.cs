using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDestroy : MonoBehaviour
{
    public void DestroyIngredientInstance()
    {
        Destroy(this.gameObject);
    }
}
