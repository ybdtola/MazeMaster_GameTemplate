using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelComplete : MonoBehaviour
{

    public static LevelComplete instance;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI cheeseText;
    public GameObject levelcomplete;

    //[SerializeField] string levelToLoad;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        levelcomplete.SetActive(false);

    }

    public void UpdateUI()
    {
        levelcomplete.SetActive(true);
        cheeseText.text = GameManager.instance.GetCurrentCheeseCount().ToString();
        TimerController.instance.EndTimer();
        timerText.text = TimerController.instance.getCurrentTime().ToString();
        Time.timeScale = 0f;
        AudioListener.pause = true;
        //SceneManager.LoadScene(levelToLoad);
    }
}
