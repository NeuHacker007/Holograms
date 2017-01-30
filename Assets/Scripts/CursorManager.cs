using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    public GameObject CursorOnHolograms;
    public GameObject CursorOffHolograms;


    void awake()
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
            CursorOnHolograms.SetActive(false);
        }

        gameObject.transform.position = GazeManager.Instance.Position;
        gameObject.transform.up = GazeManager.Instance.Normal;
    }


}
