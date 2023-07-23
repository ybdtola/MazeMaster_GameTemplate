using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;
    GameObject cheesy;

    [SerializeField] float PlayerChaseRange;
    Vector3 sd;
    // Start is called before the first frame update
    void Start()
    {
        //find the player
        target = GameObject.Find("Player");

        cheesy = GameObject.FindWithTag("target");
        agent = GetComponent<NavMeshAgent>();
        Vector3 newPosition = Random.insideUnitCircle * 5;
        Debug.Log(newPosition + "Scholar Dami oooo");

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(agent.transform.position, PlayerChaseRange);
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
        if (Vector3.Distance(agent.transform.position, target.transform.position) < 4)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
       
        //agent.transform.position = Random.insideUnitCircle;
        //Debug.Log(agent.transform.position + "ahhhhhh");
    }

    void Pursue()
    {
        Vector3 targetDir = target.transform.position - agent.transform.position;

        float relativeHeading = Vector3.Angle(agent.transform.forward, agent.transform.TransformVector(agent.transform.forward));

        float toTarget = Vector3.Angle(agent.transform.forward, agent.transform.TransformVector(targetDir));

        if(toTarget > 90 && relativeHeading < 20 || target.GetComponent<Move>().moveSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }

        if(target.GetComponent<Move>().moveSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Move>().moveSpeed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Update()
    {
        if (Vector3.Distance(agent.transform.position, cheesy.transform.position) > 4)
        {
            Seek(cheesy.transform.position);
        }
        if (Vector3.Distance(agent.transform.position, target.transform.position) < PlayerChaseRange)
        {
        Pursue();
        }
        else
        {
            Debug.Log("Player not in Range");
          
        }
        //spawnCheese.GetCheeseCurrentLocation();
        

    }
    void LateUpdate()
    {
        cheesy = GameObject.FindWithTag("target");
    }
}
