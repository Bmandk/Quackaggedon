using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterClickEffectController : MonoBehaviour
{

    public GameObject waterClickPrefab;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.transform.CompareTag("ClickableWater"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = -Camera.main.transform.position.z; // zero z

                Instantiate(waterClickPrefab, mouseWorldPos, waterClickPrefab.transform.rotation);
            }
        }
    }
}
