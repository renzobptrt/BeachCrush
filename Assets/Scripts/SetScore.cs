using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetScore : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
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
            currentScoreText.text = score.ToString();
        }
    }

    void Start()
    {
        Score = PlayerPrefs.GetInt("currentScore");
    }
}
