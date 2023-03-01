using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private Text currentScore, highScore;
    private int currentPoints, highScorePoints;

    private void Start()
    {
        currentPoints = 0;
        LoadHighScore();
    }

    public void UpdateCurrentScore(int newPointValue)
    {
        currentPoints += newPointValue;
        currentScore.text = "Score: " + currentPoints;
        if (currentPoints > highScorePoints) { UpdateHighScore(currentPoints); }
    }

    public void UpdateHighScore(int newHighScoreValue)
    {
        highScorePoints = newHighScoreValue;
        highScore.text = "High Score: " + highScorePoints;
    }

    public void SaveHighScore()
    {
        if (currentPoints >= highScorePoints) PlayerPrefs.SetInt("HighScore", highScorePoints);
    }

    public void LoadHighScore()
    {
        UpdateHighScore(PlayerPrefs.GetInt("HighScore"));
    }
}
