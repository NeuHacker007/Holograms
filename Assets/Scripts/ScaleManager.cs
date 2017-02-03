using UnityEngine;

public class ScaleManager : MonoBehaviour
{

    public float maxScale = 10.0f;
    public float minScale = 2.0f;
    public float shirnkSpeed = 1.0f;


    private float targetScale;
    private Vector3 v3Vector3;
    // Use this for initialization
    void Start()
    {
        v3Vector3 = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                targetScale = minScale;
                v3Vector3 = new Vector3(targetScale, targetScale, targetScale);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                targetScale = maxScale;
                v3Vector3 = new Vector3(targetScale, targetScale, targetScale);
            }
        }

        transform.localScale = Vector3.Lerp(transform.localScale, v3Vector3, Time.deltaTime * shirnkSpeed);
    }
}
