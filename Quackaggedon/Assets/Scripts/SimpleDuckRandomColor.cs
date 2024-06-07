using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDuckRandomColor : MonoBehaviour
{
    public Color[] bodyColor;
    public Color[] beakColor;

    [SerializeField]
    private SpriteRenderer head, neck, body, footL, footR, beakClosed, beakOpened;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomSimpleDuckColor();
    }

    private void SetRandomSimpleDuckColor()
    {
         int randomIndex = UnityEngine.Random.Range(0, bodyColor.Length);

        head.color = bodyColor[randomIndex];
        neck.color = bodyColor[randomIndex];
        body.color = bodyColor[randomIndex];

        footL.color = beakColor[randomIndex];
        footR.color = beakColor[randomIndex];
        beakClosed.color = beakColor[randomIndex];
        beakOpened.color = beakColor[randomIndex];
    }
}
