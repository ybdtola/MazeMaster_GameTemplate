using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelExit : MonoBehaviour
{

    //public TextMeshProUGUI timerText;
    //public TextMeshProUGUI cheeseText;




    //[SerializeField] string levelToLoad;
    GameObject doorInstance;

    void Start()
    {
        //levelcomplete.SetActive(false);
        doorInstance = GameObject.Find("Door(Clone)");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Goooall");
            LevelComplete.instance.UpdateUI();
            //levelcomplete.SetActive(true);
            //cheeseText.text = GameManager.instance.GetCurrentCheeseCount().ToString();
            //TimerController.instance.EndTimer();
            //timerText.text = TimerController.instance.getCurrentTime();
            ////SceneManager.LoadScene(levelToLoad);
        }
    }
}
