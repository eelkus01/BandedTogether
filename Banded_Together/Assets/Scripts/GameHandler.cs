using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public GameObject scoreText;
    private int playerScore = 0;

    void Start()
    {
        UpdateScore();
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        Text scoreTextB = scoreText.GetComponent<Text>();
        scoreTextB.text = "SCORE: " + playerScore;
    }
}
