using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HutDuckIconSetter : MonoBehaviour
{
    public DuckType duckType;
    public Image duckImage;

    public Color hiddenColor;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (DiscoveredObjects.HasSeenDuck(duckType) || SaveManager.DidPlayerFinishGame())
        {
            duckImage.color = Color.white;
            duckImage.sprite = References.Instance.GetDuckData(duckType).duckDisplayMiniIcon;
        }
        else 
        {
            duckImage.color = hiddenColor;
            duckImage.sprite = References.Instance.GetDuckData(duckType).duckDisplayMiniHidden;
        }
    }

}
