using UnityEngine;
using UnityEngine.AI;

public class GuopinMovement_navmesh : MonoBehaviour
{
    //create two variable which take parameter from Unity 3D
    NavMeshAgent NavaAgent;
    public Transform destination; // for specifying the destination position for guopin character

    // Use this for initialization
    void Start()
    {
        NavaAgent = GetComponent<NavMeshAgent>(); // get NavMeshAgent object from Unity 3D
        NavaAgent.destination = destination.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
