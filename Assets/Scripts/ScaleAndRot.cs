using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndRot : MonoBehaviour
{

    public Color SelectColor = new Color32(98,99,255,128);
    public Color ScaleColor = new Color32(98, 159, 255, 128);
    private GameObject SelectorBox;
    private GameObject ScaleHandleBox;
    public static Material SelectMaterial;
    public static Material ScaleMaterial;

    void Start()
    {
        SelectorBox = GameObject.Find("SelectorMat");
        ScaleHandleBox = GameObject.Find("ScaleMat");
        SelectMaterial = SelectorBox.GetComponent<Renderer>().material;
        SelectMaterial.color = SelectColor;

        ScaleMaterial = ScaleHandleBox.GetComponent<Renderer>().material;
        ScaleMaterial.color = ScaleColor;
    }
}
