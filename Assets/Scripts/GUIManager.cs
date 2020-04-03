using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public static GUIManager sharedInstance;
    private GameObject findGM;
    public TextMeshProUGUI movesCounterText, scoreText;
    private int movesCounter;
    private int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = "Score:  " + score;
        }
    }

    public int MovesCounter
    {
        get
        {
            return movesCounter;
        }
        set
        {
            movesCounter = value;
            movesCounterText.text = "Moves: " + movesCounter;
            if (movesCounter <= 0)
            {
                movesCounter = 0;
                StartCoroutine(GameOver());
            }
        }
    }

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(this.gameObject);
        findGM = GameObject.Find("FindGM");
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Score = 0;
        MovesCounter = 30;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitUntil(() => !BoardManager.sharedInstance.isShifting);
        yield return new WaitForSeconds(0.25f);
        //Go To GameOver Scene
        PlayerPrefs.SetInt("currentScore", score);
        findGM.GetComponent<FindGM>().GoToGameOver();
    }
}
