using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPoolHandler : MonoBehaviour
{
    private static CleanPoolHandler _instance;

    public static CleanPoolHandler Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject dirtInPool;
    public GameObject[] dirtInteractors;

    public void DisableDirt()
    {
        AudioController.Instance.EnableBackgroundMusic();
        Destroy(dirtInPool);
        foreach (var dirt in dirtInteractors)
            Destroy(dirt);
        References.Instance.menuController.EnabbleUiForDirtInteraction();
        //Enable regular UI
    }

    public void EnableDirt()
    {
        AudioController.Instance.DisableBackgroundMusic();
        dirtInPool.SetActive(true);

        foreach (var dirt in dirtInteractors)
            dirt.SetActive(true);

        References.Instance.menuController.DisableUiForDirtInteraction();
        //Disable regular UI
    }
}
