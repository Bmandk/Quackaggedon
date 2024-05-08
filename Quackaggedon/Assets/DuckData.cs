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
        Magical,
        Muscle,
        Clever,
        LunchLady,
    }

    public DuckType duckType;
    public string duckDisplayName;
    public Sprite duckDisplayIcon;
    
}
