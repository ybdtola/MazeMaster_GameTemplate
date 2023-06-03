using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using static UnityEditor.PlayerSettings;
using Grpc.Core;


public class MoveToGoal : Agent
{

    Rigidbody m_AgentRb;
    [SerializeField] public GameObject target;

    //GameObject cheesy;
    private SpawnController spawnCheese;

    //EnvironmentParameters m_Curricula;

    //float DefaultLocation = 0.5f;

    private MazeArea getAreaBound;
    float xFirst, zFirst, xLast, zLast;

   

    Vector3 cheeseCurrentLocation;

    public int Episode = 0;
    public int Success = 0;
    public int Fail = 0;
    public int TimeStep = 0;
    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        spawnCheese = GetComponent<SpawnController>();
        getAreaBound = GetComponent<MazeArea>();
        //m_Curricula = Academy.Instance.EnvironmentParameters;

    }
    void Start()
    {
        var FirstItemCoordinate = getAreaBound.GetAreaBoundsFirst();
        xFirst = FirstItemCoordinate.Item1;
        zFirst = FirstItemCoordinate.Item2;
        Debug.Log(xFirst + ", " + zFirst);

        var LastItemCoordinate = getAreaBound.GetAreaBoundsLast();
        xLast = LastItemCoordinate.Item1;
        zLast = LastItemCoordinate.Item2;
        Debug.Log(xLast + ", " + zLast);

        cheeseCurrentLocation = spawnCheese.GetCheeseCurrentLocation();
    }

    void ResetCheese()
    {
        spawnCheese.SpawnCheese(target);
    }

    void AgentReset()
    {
        Vector3 pos = this.transform.localPosition;
        float x = this.transform.position.x;
        float z = this.transform.position.z;

        if (x < xFirst)
        {
            x = xFirst;
        }
        else if (x > xLast)
        {
            x = xLast;
        }
        else if (z < zFirst)
        {
            z = zFirst;
        }
        else if (z > zLast)
        {
            z = zLast;
        }
        else
        {
            pos = new Vector3(x, 0.5f, z);
        }
        //switch (pos)
        //{
        //    case x < xFirst:
        //        x = xFirst;
        //        break;
        //    case x > xLast:
        //        x = xLast;
        //        break;
        //    case z < zFirst:
        //        z = zFirst;
        //        break;
        //    case z > zLast:
        //        z = zLast;
        //        break;

        //}
    }

    public override void OnEpisodeBegin()
    {
        Episode += 1;
        //Vector3 direction = Vector3.Normalize(this.transform.position - target.transform.position);
        //target.transform.position += direction * m_Curricula.GetWithDefault("m_cheese_location", DefaultLocation);
        //transform.localPosition = Vector3.zero;
        m_AgentRb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360), 0f));

        //cheeseCurrentLocation = spawnCheese.GetCheeseCurrentLocation();
        ScreenText();

    }

    void ScreenText()
    {
        float SuccessPercent = (Success / (float)(Success + Fail)) * 100;
        Debug.Log("Episode= " + Episode + " || " + "Success= " + Success + " || " + "Fail= " + Fail + " || " + SuccessPercent + "%" + " || " + "Time: " + TimeStep);

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        //Vector3 direction = Vector3.Normalize(this.transform.position - target.transform.position);
        //sensor.AddObservation(direction);
        //sensor.AddObservation(target.transform.position);

        sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));
        //Debug.Log(cheeseCurrentLocation + "cheesy yo!");

        Vector3 currentPosForward = transform.TransformDirection(Vector3.forward);

        Vector3 dirToCheese = (target.transform.localPosition - transform.localPosition).normalized;
        //Debug.Log(dirToCheese);
        float angleBetween = Vector3.Angle(currentPosForward, dirToCheese);

        sensor.AddObservation(dirToCheese);

    }

    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 150f);
        m_AgentRb.AddForce(dirToGo * 3f, ForceMode.VelocityChange);


        //Vector3 currentPosForward = transform.TransformDirection(Vector3.forward);
        ////cheeseCurrentLocation = spawnCheese.GetCheeseCurrentLocation();

        float distance = Vector3.Distance(target.transform.position, transform.position);
        //{

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3.0f))
        {
            //print("Found an object - distance: " + hit.distance);
            if (hit.collider.gameObject.CompareTag("wall"))
            {
                AddReward(-0.01f);
                Fail += 1;
            }

        }
        if (Vector3.Distance(target.transform.position, transform.position) < 6.0f)
        {
            AddReward(0.05f);
            Success += 1;
        }
        Vector3 targetDir = target.transform.position - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, targetDir);
        if (angleBetween > 40)
        {
            AddReward(-0.01f);
            Fail += 1;
        }

        //if (transform.position.x < xFirst || transform.position.x > xLast || transform.position.z < zFirst || transform.position.z > zLast)
        //{
        //    AddReward(-0.01f);
        //    //Debug.Log("Lose");
        //    this.transform.localPosition = new Vector3(0f, 1f, 0f);
        //    //this.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
        //    //AgentReset();
        //    //EndEpisode();
        //}
        //if (transform.position.y > 1f || transform.position.y < 1f)
        //{
        //    AddReward(-0.01f);
        //    //Debug.Log("Lose");
        //    float y = this.transform.localPosition.y;
        //    y = 1f;
        //    //this.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
        //    //AgentReset();
        //    //EndEpisode();
        //}
    }
    void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * 5.0f, Color.yellow);
        Vector3 targetDir = target.transform.position - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, targetDir);
        //Debug.Log(angleBetween);
        Debug.DrawRay(this.transform.position, targetDir, Color.red, 5);

    }

    void LateUpdate()
    {
        TimeStep += 1;
    }


    //void FixedUpdate()
    //{
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit");
    //        AddReward(0.5f);
    //    }
    //}

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(-0.05f / MaxStep); // old -> -0.05f...add later
        MoveAgent(actionBuffers.DiscreteActions);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            Success += 1;
            SetReward(1f);
            Debug.Log("Win");
            goal.gameObject.SetActive(false);
            ResetCheese();
            EndEpisode();
        }
        if (other.TryGetComponent<Walls>(out Walls wall))
        {
            Fail += 1;
            SetReward(-1f);
            Debug.Log("Lose");
            EndEpisode();
        }
        //if (other.TryGetComponent<Obstacles>(out Obstacles obstacle))
        //{
        //    SetReward(-5f);
        //    floorMeshRender.material = obsMaterial;
        //    EndEpisode();
        //}

    }


}
