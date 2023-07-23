using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CollisionDetection class is responsible for detecting collision between the player and detectables(cheese and walls)
/// </summary>
public class CollisionDetection : MonoBehaviour
{
    bool collision;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        MyCollisions();
    }

    /// <summary>
    /// Function to detect collision between the player and detectables(cheese and walls)
    /// </summary>
    /// <returns></returns>
    public bool MyCollisions()
    {
        int i = 0;
        Collider[] hitColliders = Physics.OverlapBox(this.transform.position, transform.localScale / 2, Quaternion.identity);

        while (i < hitColliders.Length && hitColliders[i].name != "ml-target(Clone)")
        {
            if (hitColliders.Length > 0 )
            {
                collision = true;
                Debug.Log("Ahhhhhh");
            }
            Debug.Log("Hit : " + hitColliders[i].name + i);
            i++;
        }
        return collision;
    }
}
