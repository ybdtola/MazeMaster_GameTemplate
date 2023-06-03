using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;
    public GameObject target;
    bool autopilot = false;
    //SpawnManager sm;

    Vector3 destination;

    // Start is called before the first frame update
    private void Awake()
    {
        //sm = GetComponent<SpawnManager>();
       
        Vector3 pos = new Vector3(24f, -2f, 39.55574f);
        //38, 44  6, 18

        Instantiate(target, pos, Quaternion.identity);
        target.transform.position = pos;


    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        //destination = target.transform.position;
        //agent.destination = destination;
    }
    void Autopilot()
    {
        destination = target.transform.position;
        agent.destination = destination;
    }
    void CalculateDistance()
    {

        if (Vector3.Distance(destination, target.transform.position) > 1.0f)
        {
            Autopilot();
            Debug.Log("hahala");
        }
        if (Vector3.Distance(this.transform.position, target.transform.position) < 0.5f)
        {
            autopilot = false;
            agent.isStopped = true;
            return;
        }
        //Debug.Log(UDistance);
        //Debug.Log(targetTothis.magnitude);
        //Debug.Log(targetTothis.sqrMagnitude);

        //return distance;
    }
    // Update is called once per frame
    void Update()
    {
        autopilot = true;

        if (autopilot)
        {
            CalculateDistance();
        }

        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        agent.SetDestination(hit.point);
        //    }
        //}
    }

}