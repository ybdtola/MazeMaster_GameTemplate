using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TMPro;

public class Move : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    //[SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private CharacterController controller;
    private SpawnController spawnCheese;
    private Animator anim;
    private int count;

    public GameObject target;
    public TextMeshProUGUI countText;
    public GameObject winText;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void ResetCheese()
    {
        spawnCheese.SpawnCheese(target);
    }

    void Start()
    {
        spawnCheese = GetComponent<SpawnController>();
        spawnCheese.SpawnCheese(target);

        anim = GetComponentInChildren<Animator>();
        count = 0;
        SetUIText();
        winText.SetActive(false);
    }

    void SetUIText()
    {
        countText.text = count.ToString();
        //if (count >= 5)
        //{
        //    winText.SetActive(true);
        //    //UnityEditor.EditorApplication.isPlaying = false;
        //}
    }

    void CalculateDistance()
    {

        if (Vector3.Distance(target.transform.position, this.transform.position) < 6.0f)
        {

            ResetCheese();
            //Debug.Log("Yeah");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
    }


    private void Movement()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        Debug.Log(isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.2f;
            //Debug.Log("Below, " + velocity.y);
        }

        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            //Walk();

            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Run();

            }
            //else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            //{
            //    Run();
            //}
            else if (moveDirection == Vector3.zero)
            {
                //idle
                Idle();
            }

            moveDirection *= moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime; //calc gravity
        //Debug.Log("Gravity, " + velocity);
        controller.Move(velocity * Time.deltaTime); //apply gravity
    }

    private void Idle()
    {
        //Debug.Log("Idle");
        anim.SetFloat("Speed", 0);
    }

    //private void Walk()
    //{
    //    moveSpeed = walkSpeed;
    //    anim.SetFloat("Speed", 0.5f);
    //} 

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 0.5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            //Debug.Log("Win");
            goal.gameObject.SetActive(false);
            CalculateDistance();
            count += 1;
            SetUIText();
        }
    }
}