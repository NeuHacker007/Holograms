using UnityEngine;


public enum Rotation { Free, x, y }
public class ObjectFacetoYou : MonoBehaviour
{
    public Rotation rotation = Rotation.Free;
    public Quaternion defaultRotation { get; private set; }
    // Use this for initialization
    void Start()
    {
        defaultRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 distancetoTarget = Camera.main.transform.position - gameObject.transform.position;

        switch (rotation)
        {
            case Rotation.x:
                distancetoTarget.x = gameObject.transform.position.x;
                break;
            case Rotation.y:
                distancetoTarget.y = gameObject.transform.position.y;
                break;
            case Rotation.Free:
            default:
                break;
        }

        gameObject.transform.rotation = Quaternion.LookRotation(-distancetoTarget) * defaultRotation;
    }
}
