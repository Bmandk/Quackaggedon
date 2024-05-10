using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHandler : MonoBehaviour
{

    private Vector3 mousePosition;
    public float offset;
    public float maxY;
    public static ArmHandler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 newPos = new Vector3(mousePosition.x, Mathf.Min(mousePosition.y + offset, maxY), 0);
        transform.position = newPos;
    }

    public void ThrowFood(FoodType foodType)
    {

    }
}
