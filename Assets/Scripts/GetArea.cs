using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArea : MonoBehaviour
{
    static Maze2 m2Instance;
    static SpawnManager sp;

    void Start()
    {
        m2Instance = GetComponent<Maze2>();
        sp = GetComponent<SpawnManager>();
    }

    public (float, float) GetAreaBoundsFirst()
    {

        float firstItemPosX = sp.GetFirstItemPos(m2Instance).min.x;
        float firstItemPosZ = sp.GetFirstItemPos(m2Instance).min.z;


        return (firstItemPosX, firstItemPosZ);
    }

    public (float, float) GetAreaBoundsLast()
    {
        float lastItemPosX = sp.GetLastItemPos(m2Instance).max.x;
        float lastItemPosZ = sp.GetLastItemPos(m2Instance).max.z;


        return (lastItemPosX, lastItemPosZ);
    }



}
