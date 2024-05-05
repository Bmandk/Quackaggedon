using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFaucetOnClick : MonoBehaviour
{
    public GameObject faucetStream;
    private void OnMouseDown()
    {
        faucetStream.SetActive(!faucetStream.activeSelf);
    }
}
