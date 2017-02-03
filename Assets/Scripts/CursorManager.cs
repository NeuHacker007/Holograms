using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    /*
     * 1. physical raycast
     * 2. store the position and normal of the raycast intersection
     * 3. If raycast doesn't hit, set position to default
     */
    public GameObject CursorOnHolograms;
    public GameObject CursorOffHolograms;

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

        if (GazeManager.Instance.Hit)
        {
            CursorOnHolograms.SetActive(true);
            CursorOffHolograms.SetActive(false);
        }
        else
        {
            CursorOffHolograms.SetActive(true);
            CursorOnHolograms.SetActive(true);
        }

        gameObject.transform.position = GazeManager.Instance.Position;
        gameObject.transform.up = GazeManager.Instance.Normal;

    }


}
