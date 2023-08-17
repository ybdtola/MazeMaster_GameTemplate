using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;


    public TextMeshProUGUI timerCounter;
    private TimeSpan timePlaying;

    private bool timerGoing;

    private float elapsedTime;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        timerCounter.text = "00:00:00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "mm':'ss':'ff";
            timerCounter.text = timePlaying.ToString(timePlayingStr);
            yield return null;
        }       
    }

    public string getCurrentTime()
    {
        string newTime = timerCounter.text.ToString();
        newTime = newTime.Substring(0, 5);
        return newTime;
        //Debug.Log(timerCounter.text);
    }
}
