using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public void OnThrow()
    {
        DuckFeeder.SelectedFeeder.ThrowBread();
    }
}
