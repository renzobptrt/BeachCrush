using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGM : MonoBehaviour
{
    private GameObject gameManager;

    void Start()
    {
        if (gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void GoToMain()
    {
        gameManager.GetComponent<GameManager>().GoToMain();
    }

    public void GoToGameOver()
    {
        gameManager.GetComponent<GameManager>().GoToGameOver();
    }

    public void GoToMenu()
    {
        gameManager.GetComponent<GameManager>().GoToMenu();
    }

    public void ExitGame()
    {
        gameManager.GetComponent<GameManager>().ExitGame();
    }
}
