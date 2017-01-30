using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    /*
     * 1. physical raycast
     * 2. store the position and normal of the raycast intersection
     * 3. If raycast doesn't hit, set position to default
     */
    public float MaxGazeDistance = 0.8f;


    public Vector3 Position { get; private set; }

    public Vector3 Normal { get; private set; }

    public bool Hit { get; private set; }


    private GazeStabilizer gazeGazeStabilizer;
    private Vector3 gazeOrigin;
    private Vector3 gazeDirection;

    // Use this for initialization
    void Awake()
    {
        if (CursorOnHolograms == null || CursorOffHolograms == null)
        {
            return;
        }

        CursorOnHolograms.SetActive(false);
        CursorOffHolograms.SetActive(false);
    }

    void update()
    {
        if (GazeManager.Instance == null || CursorOffHolograms == null || CursorOnHolograms == null)
        {
            return;
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

        gazeOrigin = gazeGazeStabilizer.StableHeadPosition;

        updateRaycast();


        //using gazeStabilizer get stable head position and assign it to gazeOrigin


        //update raycast


    }

    private void updateRaycast()
    {

        // declare hitInfo

        //Perform a unity physics raycast


        //assign hitInfo to Hitinfo public property

        //if hit, set public position = hitinfo.postion and public Normal = hitinfo.Normal

        gameObject.transform.position = GazeManager.Instance.Position;
        gameObject.transform.up = GazeManager.Instance.Normal;
    }


}
