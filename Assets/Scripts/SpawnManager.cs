using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject cheese;
    GameObject[] areas;

    Bounds firstItem;
    Bounds lastItem;


    public Bounds GetFirstItemPos(Maze2 m2)
    {
        firstItem = m2.transform.GetChild(0).Find("Floor").GetComponent<Renderer>().bounds;

        return firstItem;
    }

    public Bounds GetLastItemPos(Maze2 m2)
    {
        lastItem = m2.transform.GetChild(m2.transform.childCount - 1).Find("Floor").GetComponent<Renderer>().bounds;
        Debug.Log(lastItem);
        return lastItem;
    }

    public void SpawnCheese(GameObject gO)
    {
        areas = GameObject.FindGameObjectsWithTag("Floor");
        var radn = Random.Range(0, areas.Length);

        var spawnAreaTransform = areas[radn].transform;

        var xRange = spawnAreaTransform.localScale.x / 2.0f;
        var zRange = spawnAreaTransform.localScale.z / 2.5f;

        gO.transform.position = new Vector3(Random.Range(-xRange, xRange), 0, Random.Range(-zRange, zRange))
            + spawnAreaTransform.position;

        Instantiate(gO, gO.transform.position, Quaternion.identity);

    }
}
