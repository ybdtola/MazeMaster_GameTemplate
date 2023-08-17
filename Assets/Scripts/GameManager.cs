using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int currentCheese;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    private void Start()
    {
        UIManager.instance.UpdateCheeseCountText(currentCheese);
        TimerController.instance.BeginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cheese Count: " + currentCheese);
        
    }
    public int GetCurrentCheeseCount()
    {
        return currentCheese;
    }
}
