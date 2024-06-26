using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaSettings : MonoBehaviour
{
    [SerializeField]
    private int areaIndex = 0;
    [SerializeField]
    private bool isStartingArea = false;
    [SerializeField]
    private float cameraSize = 5f;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    //private int duckLimit = 50;
    
    public int AreaIndex => areaIndex;
    public float CameraSize => cameraSize;
    public Transform[] SpawnPoints => spawnPoints;
    
    public static AreaSettings CurrentArea { get; private set; }

    private static float _startSizeReference;
    private static float _startArmOffsetReference;

    private void Awake()
    {
        CurrentArea = this;
    }

    private void Start()
    {
        if (isStartingArea)
        {
            _startSizeReference = CameraSize;
            //_startArmOffsetReference = ArmHandler.Instance.offset;
            SelectArea();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * CameraSize * 16f/9f, 2 * CameraSize, 0));
    }
    
    public void SelectArea()
    {
        /*
        CurrentArea = this;
        Camera.main.orthographicSize = CameraSize;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        //ArmController.Instance.transform.parent.position = new Vector3(transform.position.x, -CameraSize, 0);
        References.Instance.armHandler.transform.localScale = Vector3.one * (CameraSize / _startSizeReference);
        References.Instance.armHandler.offset = _startArmOffsetReference * (CameraSize / _startSizeReference);
        if (DuckFeeder.SelectedFeeder != null)
            DuckFeeder.SelectedFeeder.Refresh();
        */
    }
}