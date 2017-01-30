using UnityEngine;

public class GazeManager : Singleton<GazeManager>
{

    /*
        * 1. physical raycast
        * 2. store the position and normal of the raycast intersection
        * 3. If raycast doesn't hit, set position to default
        */
    [Tooltip("max gaze distance to calc a hit")]
    public float MaxGazeDistance = 0.8f;

    public RaycastHit HitInfo { get; private set; }

    public Vector3 Position { get; private set; }

    public Vector3 Normal { get; private set; }

    public bool Hit { get; private set; }
    [Tooltip("select the layers raycast should target")]
    public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;
    private GazeStabilizer gazeGazeStabilizer;
    private Vector3 gazeOrigin;
    private Vector3 gazeDirection;

    // Use this for initialization
    void Awake()
    {
        //get gaze stabilizer

        gazeGazeStabilizer = GetComponent<GazeStabilizer>();

    }

    // Update is called once per frame
    void Update()
    {
        // assign camera position to gaze orign
        gazeOrigin = Camera.main.transform.position;



        //assign camera's main transform forward to gaze direction
        gazeDirection = Camera.main.transform.forward;

        //Using gaze Stabilizer, call function updateheadstability

        gazeGazeStabilizer.UpdateHeadStability(gazeOrigin, Camera.main.transform.rotation);


        //using gazeStabilizer get stable head position and assign it to gazeOrigin
        gazeOrigin = gazeGazeStabilizer.StableHeadPosition;

        //update raycast
        updateRaycast();
    }

    private void updateRaycast()
    {

        // declare hitInfo
        RaycastHit hitInfo;


        //Perform a unity physics raycast
        Hit = Physics.Raycast(gazeOrigin, gazeDirection, out hitInfo, MaxGazeDistance, RaycastLayerMask);

        //assign hitInfo to Hitinfo public property
        HitInfo = hitInfo;
        //if hit, set public position = hitinfo.postion and public rmal = hitinfo.Normal

        if (Hit)
        {
            Position = hitInfo.point;
            Normal = hitInfo.normal;
        }
        else
        {
            //set position to gazeOrigin +  (gazeDirection * MaxGazeDistance);

            Position = gazeOrigin + (gazeDirection * MaxGazeDistance);
            // assign normal to gaze position.
            Normal = gazeDirection;
        }



    }
}
