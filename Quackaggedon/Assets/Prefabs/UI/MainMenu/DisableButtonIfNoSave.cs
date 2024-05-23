using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButtonIfNoSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!SaveManager.DoesSaveExist())
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
