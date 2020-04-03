using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;
    public GameObject guiManager;

    void Awake()
    {
        if (sharedInstance != null)
            Destroy(this.gameObject);
        else
        {
            sharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
