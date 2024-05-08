using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckData : MonoBehaviour
{
    public enum DuckType
    {
        Simple,
        Chef,
        Mafia,
        Bread,
        Princess,
        Muscle,
        Clever,
    }

    public DuckType duckType;
    public string duckDisplayName;
    public Sprite duckDisplayIcon;
    
}
