using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Cursor.lockState = CursorLockMode.None;
    }
}
