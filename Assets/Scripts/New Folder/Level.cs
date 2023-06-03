using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Level : MonoBehaviour
{

    public GameObject player;

    private SpawnController spawnCheese;
    public GameObject cheese;

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.localPosition;
        spawnCheese = GetComponent<SpawnController>();

        Instantiate(player, new Vector3(0f, 1f, 0f), Quaternion.identity);
        spawnCheese.SpawnCheese(cheese);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
