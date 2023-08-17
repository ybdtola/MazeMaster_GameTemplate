using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
