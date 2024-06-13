using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;
using UnityEngine.UIElements;

public class DuckData : MonoBehaviour
{
    public DuckType duckType;
    public string duckDisplayName;
    [TextArea(5, 20)]
    public string duckEffectDescription;
    public Sprite duckDisplayIcon;
    public Sprite duckDisplayMiniIcon;
    
    /*    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠠⠄⠒⠐⠈⠉⠉⠉⠉⠁⠂⠐⠢⠄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⡠⠐⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⡉⠒⢄⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⢀⠔⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⣿⠿⣿⣶⡀⠈⠢⡀⠀⠀⠀⠀
        ⠀⠀⠀⡠⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⣿⠟⠉⠀⠀⠈⠿⠇⠀⠀⠘⢄⠀⠀⠀
        ⠀⠀⡰⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠀⠀
        ⠀⠰⠁⠀⠀⠀⠀⣠⣾⣿⠿⣿⣶⣤⣤⣴⡶⠀⠀⠀⠀⠀⠀⢀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡆⠀
        ⠀⡇⠀⠀⠀⠀⠀⠿⠋⠀⢀⣠⣍⡉⠉⠉⠀⠀⠀⠀⠀⠀⣴⣿⣿⣦⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀
        ⢠⠁⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆
        ⢸⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠐⡇
        ⠘⡄⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠃
        ⠀⢇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡿⠀
        ⠀⠘⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣞⠃⠀
        ⠀⠀⠘⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⣿⣿⣿⣿⣿⣷⡆⠀⠀⠀⠀⠀⠀⠀⠀⢀⣞⠏⠀⠀
        ⠀⠀⠀⠘⢄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠉⠋⠉⠉⠉⠙⠉⠁⠀⠀⠀⠀⠀⠀⠀⡤⡫⠁⠀⠀⠀
        ⠀⠀⠀⠀⠀⠑⢤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⢴⡫⠊⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠈⠒⠤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡤⣴⠮⠓⠁⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠒⠒⠶⠲⠶⠶⠶⠮⠙⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
    public GameObject duckPrefab;
    public GameObject duckInstance;

    public static Dictionary<DuckType, List<DuckData>> duckObjects;

    private void Awake()
    {
        duckObjects[duckType].Add(this);
    }
}
