using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CusorManager : Singleton<CusorManager>
{

    public GameObject CursorOnHolograms;
    public GameObject CursorOffHolograms;

    public float distance = 0.01f;

    private void Awake()
    {
        if (CursorOnHolograms != null)
        {
            CursorOnHolograms.SetActive(true);
        }
        if (CursorOffHolograms !=null)
        {
            CursorOffHolograms.SetActive(true);
        }

        if (GazeManager.Instance == null)
        {
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (CursorOnHolograms != null)
        {
            CursorOnHolograms.SetActive(GazeManager.Instance.IsGazingAtObject);  
        }
        if (CursorOffHolograms != null)
        {
            CursorOffHolograms.SetActive(!GazeManager.Instance.IsGazingAtObject);
        }
    }
}
