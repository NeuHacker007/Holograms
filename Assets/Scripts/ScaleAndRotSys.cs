using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using HoloToolkit.Unity.InputModule;

public class ScaleAndRotSys : MonoBehaviour
{

    private GameObject SARObj;
    private Renderer renderer;
    //private Material SARSelectorMaterial;
    //private Material sarScaleMaterial;

    private Bounds SARBounds;

    private bool draw = false;
    public Color SelectColor;
    public Color ScaleColor;
    private GameObject SelectorBox;
    private GameObject ScaleHandleBox;
    private GameObject VideoPlayWall;
    public static Material SelectMaterial;
    public static Material ScaleMaterial;

    // Use this for initialization
    void Start()
    {
        SelectorBox = GameObject.Find("SelectorMat");
        ScaleHandleBox = GameObject.Find("ScaleMat");
        SelectMaterial = SelectorBox.GetComponent<Renderer>().material;
        SelectMaterial.color = SelectColor;

        ScaleMaterial = ScaleHandleBox.GetComponent<Renderer>().material;
        ScaleMaterial.color = ScaleColor;
        SARBounds = new Bounds();
        //SARSelectorMaterial = SelectMaterial;
        //sarScaleMaterial = ScaleMaterial;

        VideoPlayWall = GameObject.Find("VideoPlayWall");
        OnSelect();
    }

    void OnMouseDown()
    {
        
    }

    void OnSelect()
    {
        if (!draw)
        {
            DrawSelectionBox();
            draw = true;
        }
        else if (draw && gameObject.transform.childCount != 0 && gameObject.tag != "Cursor")
        {
            DeleteAllChildren();
            draw = false;
        }
    }

    void DeleteAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


    void DrawSelectionBox()
    {
        //1. create box;
        SARObj = GameObject.CreatePrimitive(PrimitiveType.Quad);

        //2. set its position to the current objects position
        SARObj.transform.position = VideoPlayWall.transform.position;
        //3. set the boxes parent to this object.
        SARObj.transform.parent = VideoPlayWall.transform;
        //4.Destroy the boxes Collider
        Destroy(SARObj.GetComponent<Collider>());
        //5. get a reference to the boxes renderer
        renderer = SARObj.GetComponent<Renderer>();
        //6. using the parent objects meshfilter, get a reference to the objects bounds
        SARBounds = VideoPlayWall.GetComponent<MeshFilter>().mesh.bounds;
        Vector3[] verticles = VideoPlayWall.GetComponent<MeshFilter>().mesh.vertices;
        float length = SARBounds.size.x;
        float width = SARBounds.size.y;
        float height = SARBounds.size.z;
        //7. Scale the box object to be a little bigger than the objects
        SARObj.transform.localScale = new Vector3(length * 1.1f, width * 1.1f, height * 1.1f);

        foreach (Vector3 vetex in verticles)
        {
            CreateEndPoints(transform.TransformPoint(vetex *1.05f));
        }
        //Vector3 p1 = verticles[0] * 1.05f;
        //Vector3 p2 = verticles[1] * 1.05f;
        //Vector3 p3 = verticles[2] * 1.05f;
        //Vector3 p4 = verticles[3] * 1.05f;

        //Debug.DrawLine(p1,p3,Color.blue,10);
        //Debug.DrawLine(p2, p3, Color.blue,10);
        //Debug.DrawLine(p2, p4, Color.blue, 10);
        //Debug.DrawLine(p1, p4, Color.blue, 10);
        // apply material
        renderer.material = SelectMaterial;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void CreateEndPoints(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(cube.GetComponent<Collider>());
        cube.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        cube.AddComponent<HandDraggable>();
        cube.transform.position = position;
        cube.transform.parent = this.transform;
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material = ScaleMaterial;
    }
}
