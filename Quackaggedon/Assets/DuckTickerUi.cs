using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckTickerUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnTicks),1,1);   
    }

    private void SpawnTicks()
    {
        References.Instance.sceneDataHolder.MakeAllDucksEmitTick();
    }
}
