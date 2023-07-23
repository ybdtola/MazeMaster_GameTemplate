using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] areas;

    Bounds firstItem;
    Bounds lastItem;


    public Bounds GetFirstItemPos(Level first_level)
    {
        firstItem = first_level.transform.GetChild(0).Find("Floor").GetComponent<Renderer>().bounds;

        Debug.Log(firstItem + "First Item");
        return firstItem;
    }

    public Bounds GetLastItemPos(Level last_level)
    {
        lastItem = last_level.transform.GetChild(last_level.transform.childCount - 1).Find("Floor").GetComponent<Renderer>().bounds;
        return lastItem;
    }

    public void SpawnCheese(GameObject cheese)
    {
        areas = GameObject.FindGameObjectsWithTag("Floor");
        var radn = Random.Range(0, areas.Length);

        var spawnAreaTransform = areas[radn].transform;

        var xRange = spawnAreaTransform.localScale.x / 2.0f;
        var zRange = spawnAreaTransform.localScale.z / 2.5f;

        cheese.transform.position = new Vector3(Random.Range(-xRange, xRange), 0, Random.Range(-zRange, zRange))
            + spawnAreaTransform.position;

        Instantiate(cheese, cheese.transform.position, Quaternion.identity);

    }

    //public Vector3 GetCheeseCurrentLocation()
    //{
    //    Vector3 loc = GameObject.FindGameObjectWithTag("target").transform.position;
    //    Debug.Log($"{loc} <--- Cheese Here!");
    //    return GameObject.FindGameObjectWithTag("target").transform.position;
    //}
}

