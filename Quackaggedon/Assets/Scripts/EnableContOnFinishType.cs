using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class EnableContOnFinishType : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string achievement = "WIN";
        SteamUserStats.GetAchievement(achievement, out bool achieved);

        if (!achieved)
        {
            SteamUserStats.SetAchievement(achievement);
            SteamUserStats.StoreStats();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
