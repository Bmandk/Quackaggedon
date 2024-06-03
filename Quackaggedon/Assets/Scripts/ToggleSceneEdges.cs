using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSceneEdges : MonoBehaviour
{
    public GameObject edgesUI;
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        edgesUI.SetActive(true);
#endif
    }
}
