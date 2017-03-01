using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
	private Camera attachedCamera;
	public Shader postOutline;
	public Shader DrawSimple;
	private Camera tempCam;
	private Material postMat;
	// Use this for initialization
	void Start ()
	{
		attachedCamera = Camera.main;
		tempCam = new GameObject().AddComponent<Camera>();
		tempCam.enabled = false;
		postMat = new Material(postOutline);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		tempCam.CopyFrom(attachedCamera);
		tempCam.clearFlags = CameraClearFlags.Color;
		tempCam.backgroundColor = Color.black;
		tempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");
		//make the temporary rendertexture
		RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

		//put it to video memory
		TempRT.Create();

		//set the camera's target texture when rendering
		tempCam.targetTexture = TempRT;

		//render all objects this camera can render, but with our custom shader.
		tempCam.RenderWithShader(DrawSimple, "");

		//copy the temporary RT to the final image
		Graphics.Blit(TempRT, destination,postMat);
		
		//release the temporary RT
		TempRT.Release();
	

}

// Update is called once per frame
void Update () {
		
	}
}
