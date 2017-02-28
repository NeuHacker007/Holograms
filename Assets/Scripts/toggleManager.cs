using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleManager : MonoBehaviour
{

    public bool _bIsSelected = true;

    void OnDrawGizmos()
    {
        if (_bIsSelected)
        {
            OnDrawGizmosSelected();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position,gameObject.GetComponent<Renderer>().bounds.size);
        if (gameObject.GetComponent<Renderer>() != null)
        {
            Gizmos.DrawWireCube(transform.position,gameObject.GetComponent<Renderer>().bounds.size);
        }
    }
}
