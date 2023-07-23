using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArea : MonoBehaviour
{
    // Start is called before the first frame update
    static Level level_instance;
    static SpawnController spawn_cheese;

    void Awake()
    {
    }

    void Start()
    {
        spawn_cheese = GetComponent<SpawnController>();
        level_instance = GetComponent<Level>();
    }

    public (float, float) GetAreaBoundsFirst()
    {

        float firstItemPosX = spawn_cheese.GetFirstItemPos(level_instance).min.x;
        float firstItemPosZ = spawn_cheese.GetFirstItemPos(level_instance).min.z;


        return (firstItemPosX, firstItemPosZ);
    }

    public (float, float) GetAreaBoundsLast()
    {
        float lastItemPosX = spawn_cheese.GetLastItemPos(level_instance).max.x;
        float lastItemPosZ = spawn_cheese.GetLastItemPos(level_instance).max.z;


        return (lastItemPosX, lastItemPosZ);
    }



}
