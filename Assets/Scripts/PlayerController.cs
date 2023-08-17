using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //public float speed = 0;
    ////public TextMeshProUGUI countText;
    ////public GameObject winText;

    //private Rigidbody rb;
    //private float movementX;
    //private float movementY;
    //private int count;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    //count = 0;

    //    //SetUIText();
    //    //winText.SetActive(false);
    //}


    ////void SetUIText()
    ////{
    ////    countText.text = "Count: " + count.ToString();
    ////    if(count >= 4) { 
    ////        winText.SetActive(true);
    ////    }
    ////}
    //void OnMove(InputValue movementValue)
    //{
    //    Vector2 movementVector = movementValue.Get<Vector2>();

    //    movementX = movementVector.x;
    //    movementY = movementVector.y;
    //}

    //void FixedUpdate()
    //{
    //    Vector3 movementV3 = new Vector3(movementX, 0.0f, movementY);
    //    rb.AddForce(movementV3 * speed);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("PickUp"))
    //    other.gameObject.SetActive(false);
    //    //count += 1;
    //    //SetUIText();

    //}

    //public TextMeshProUGUI countText;

    //private int CheeseCount;
    //public static PlayerController instance;

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(this);
    //}
    //public void UpdateCheese()
    //{
    //    //get cheesecount from move script

    //    countText.text = GetComponent<Move>().cheeseCount.ToString();
    //    Debug.Log(countText.text + "oh boy!!!");

    //}

}
