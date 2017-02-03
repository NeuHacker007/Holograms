using UnityEngine;

public class Interactable : Singleton<Interactable>
{

    public GameObject forcusedGameObject { get; private set; }

    public GameObject oldFocusGameObject = null;
    // Use this for initialization
    void Start()
    {
        forcusedGameObject = null;

    }

    // Update is called once per frame
    void Update()
    {
        oldFocusGameObject = forcusedGameObject;
        if (GazeManager.Instance.Hit)
        {
            RaycastHit hitInfo = GazeManager.Instance.HitInfo;
            if (hitInfo.collider != null)
            {
                forcusedGameObject = hitInfo.collider.gameObject;
            }
            else
            {
                forcusedGameObject = null;
            }
        }
        else
        {
            forcusedGameObject = null;
        }

        if (forcusedGameObject != oldFocusGameObject)
        {
            if (forcusedGameObject != null)
            {
                if (forcusedGameObject.GetComponent<Interactable>() != null)
                {
                    forcusedGameObject.SendMessage("GazeEntered");
                }
            }
        }
    }

    private void ResetFocuseInteractivle()
    {
        if (oldFocusGameObject != null)
        {
            if (oldFocusGameObject.GetComponent<Interactable>() != null)
            {
                oldFocusGameObject.SendMessage("GazeExited");
            }
        }
    }
}
