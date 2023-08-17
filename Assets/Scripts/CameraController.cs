using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject player;
    //private Vector3 offset;

    [SerializeField] private float mouseSensitivity;

    private Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - player.transform.position;
        parent = transform.parent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + offset;
        Rotate();
    }

    private void  Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);
    }
}
