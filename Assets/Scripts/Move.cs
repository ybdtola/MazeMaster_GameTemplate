using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

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
    private MazeCellObject mco;

    private Animator anim;
    private int count;  

    public GameObject target;

    public TextMeshProUGUI countText;
    public GameObject winUI;
    GameObject doorInstance;
    GameObject mc;
    GameObject gm;
    Scene scene;

    [SerializeField] int cheeseCount;

    //public static Move instance;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        //if (instance != null && instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    instance = this;
        //}
        //DontDestroyOnLoad(this);
    }

    void ResetCheese()
    {
        spawnCheese.SpawnCheese(target);
    }

    void Start()
    {
        //TimerController.instance.BeginTimer();

        mc = GameObject.Find("MazeObject");
        doorInstance = GameObject.Find("Door(Clone)");
        doorInstance.SetActive(false);

        spawnCheese = GetComponent<SpawnController>();
        spawnCheese.SpawnCheese(target);

        scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name + " " + scene.buildIndex);
        


        anim = GetComponentInChildren<Animator>();
        count = 0;
        SetUIText();
        //winText.SetActive(false);
       
    }

    public void QuitScreen()
    {
        StartCoroutine(QuitWinUI());
    }
    IEnumerator QuitWinUI()
    {
        yield return new WaitForSeconds(2);
        winUI.SetActive(false);

    }
    void SetUIText()
    {
        countText.text = count.ToString();
        if (count == 2 && scene.name == "Level 1")
        {
            //winText.SetActive(true);
            winUI.SetActive(true);
            //Time.timeScale = 0f;
            //SceneManager.LoadScene(4);

            for (int i = 0; i < mc.transform.childCount; i++)
            {
                gm = mc.transform.GetChild(mc.transform.childCount - 1).gameObject;
                Debug.Log(gm.name);
            }
            //get script from gm
            GameObject l = gm.GetComponent<MazeCellObject>().GetNorthWall().gameObject;
            l.SetActive(false);
            Debug.Log(l.name);
           

            doorInstance.SetActive(true);

            //foreach (Transform child in mc.transform)
            //{
            //    Debug.Log(child.name);
            //}
            Debug.Log(mc);
            QuitScreen();
        }
            UpdateCheeseCount();
            //UnityEditor.EditorApplication.isPlaying = false;

    }

    private void UpdateCheeseCount()
    {
        UIManager.instance.cheeseText.text = cheeseCount.ToString();
        GameManager.instance.currentCheese = cheeseCount;
        Debug.Log(cheeseCount.ToString() + "the update cheese count fx");
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
            FindObjectOfType<AudioManager>().Play("Item Pickup");
            goal.gameObject.SetActive(false);
            CalculateDistance();
            count += 1;
            cheeseCount += 1;
            SetUIText();
        }
    }
}