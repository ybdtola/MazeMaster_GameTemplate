using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;
    GameObject cheesy;

    [SerializeField] float PlayerChaseRange;
    Vector3 sd;
    Vector3 wanderDirection;
    [SerializeField] bool shouldWander;
    float pauseCounter, wanderCounter;
    [SerializeField] float pauseLength, wanderLength;
    [SerializeField] private LayerMask groundMask;
    private SpawnController spawnCheese;
    Animator animator;

    bool isAttacking;
    //public GameObject winUI;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        cheesy = GameObject.FindWithTag("target");
        spawnCheese = GetComponent<SpawnController>();
        agent = GetComponent<NavMeshAgent>();
        //find the player
        //newPosition = Random.insideUnitCircle * 2;
        //Debug.Log(newPosition + "Scholar Dami oooo");
        //if (shouldWander)
        //{
        //    pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        //}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(agent.transform.position, PlayerChaseRange);
    }

    void Wander()
    {

        //float wanderRadius = 10;
        //float wanderDistance = 10;
        //float wanderJitter = 10;

        //Vector3 wanderTarget = new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);
        //wanderTarget.Normalize();
        //wanderTarget *= wanderRadius;

        //Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);

        //Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        //agent.SetDestination(targetWorld);



        Vector3 targetLocal = Vector3.zero;
        if (shouldWander)
        {
            if (wanderCounter > 0)
            {
                wanderCounter -= Time.deltaTime;
                agent.SetDestination(wanderDirection);
                animator.SetBool("isRunning", true);

                if (wanderCounter <= 0)
                {
                    //shouldWander = false;
                    animator.SetBool("isRunning", false);

                    pauseCounter = pauseLength;
                    //targetLocal = wanderDirection + new Vector3(0, 0, wanderLength);
                }
            }
            if (pauseCounter > 0)
            {

                pauseCounter -= Time.deltaTime;

                if (pauseCounter <= 0)
                {
                    //shouldWander = true;
                    animator.SetBool("isRunning", true);

                    wanderCounter = wanderLength;
                    wanderDirection = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    //wanderDirection.Normalize();
                }
            }
        }

        //float randomX = Random.Range(-5f, 5f);
        //float randomZ = Random.Range(-5f, 5f);

        //wanderDirection = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //if (Physics.Raycast(wanderDirection, Vector3.down, 2f, groundMask))
        //{
        //    shouldWander = true;
        //    agent.SetDestination(wanderDirection);
        //    Debug.DrawRay(wanderDirection, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //    Debug.Log("At the Top");
        //}
    }



    void Seek(Vector3 location)
    {

        agent.SetDestination(location);
        if (Vector3.Distance(agent.transform.position, target.transform.position) < 3.5)
        {
            agent.transform.LookAt(target.transform.position);
            //gameObject.GetComponent<Animator>().Play("Run|Attack"); 
            agent.transform.position = new Vector3(agent.transform.position.x, 0, agent.transform.position.z);

            onDestroy();
            isAttacking = false;
            agent.transform.position = new Vector3(agent.transform.position.x, 0f, agent.transform.position.z);
        }

        //agent.transform.position = Random.insideUnitCircle;
        //Debug.Log(agent.transform.position + "ahhhhhh");
    }

    void onDestroy()
    {
        if (!isAttacking)
        {
        animator.SetTrigger("isAttacking");
        FindObjectOfType<AudioManager>().Play("Death Blow");
        StartCoroutine(initializeAttack());
        }

    }

    IEnumerator initializeAttack()
    {
        yield return new WaitForSeconds(1);
        isAttacking = true;

        if (isAttacking && Vector3.Distance(agent.transform.position, target.transform.position) < 3.5)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            SceneManager.LoadScene(3);


        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "target")
    //    {
    //        animator.SetTrigger("isAttacking");
    //        UnityEditor.EditorApplication.isPlaying = false;
    //        //Debug.Log("Player is dead");
    //    }
    //}

    void Pursue()
    {
        //animator.SetBool("isRunning", true);
        animator.SetBool("isRunning", true);

        Vector3 targetDir = target.transform.position - agent.transform.position;

        float relativeHeading = Vector3.Angle(agent.transform.forward, agent.transform.TransformVector(agent.transform.forward));

        float toTarget = Vector3.Angle(agent.transform.forward, agent.transform.TransformVector(targetDir));

        if (toTarget > 90 && relativeHeading < 20 || target.GetComponent<Move>().moveSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }

        if (target.GetComponent<Move>().moveSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Move>().moveSpeed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Update()
    {
        //if (shouldWander)
        //{
        //}
        shouldWander = true;
        Wander();
        cheesy = GameObject.FindWithTag("target");

        animator.SetBool("isRunning", false);
        MoveTowardsCheeseOrPlayer();

        //spawnCheese.GetCheeseCurrentLocation();

        //Debug.Log(cheesy.transform.position = newPosition);
    }

    private void MoveTowardsCheeseOrPlayer() {
        //float DistanceToCheese = Vector3.Distance(agent.transform.position, cheesy.transform.position);
        //float DistanceToPlayer = Vector3.Distance(agent.transform.position, target.transform.position);

        if (Vector3.Distance(agent.transform.position, cheesy.transform.position) > 4)
        {
            animator.SetBool("isRunning", true);
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
        Debug.Log(cheesy.transform.position + "nawa o");

    }

    void FixedUpdate()
    {
        //pauseCounter -= Time.deltaTime;
        cheesy = GameObject.FindWithTag("target");
        //Debug.Log(pauseCounter);

    }
}