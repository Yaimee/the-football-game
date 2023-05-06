using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void PlayPractise()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(3);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
