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
    [TextArea(5, 20)]
    public string duckDetailsDescription;

    public Sprite duckDisplayIconRevealed;
    public Sprite duckDisplayIconHidden;

    public Sprite duckDisplayMiniIcon;
    public Sprite duckDisplayMiniHidden;

    
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

    public double quacksPerClick;

    private void Awake()
    {
        duckObjects[duckType].Add(this);
    }
}
