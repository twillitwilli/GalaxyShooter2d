using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private Text _currentScore, _highScore;
    private int _currentPoints, _highScorePoints;

    private void Start()
    {
        _currentPoints = 0;
        LoadHighScore();
    }

    public void UpdateCurrentScore(int newPointValue)
    {
        _currentPoints += newPointValue;
        _currentScore.text = "Score: " + _currentPoints;
        if (_currentPoints > _highScorePoints) { UpdateHighScore(_currentPoints); }
    }

    public void UpdateHighScore(int newHighScoreValue)
    {
        _highScorePoints = newHighScoreValue;
        _highScore.text = "High Score: " + _highScorePoints;
    }

    public void SaveHighScore()
    {
        if (_currentPoints >= _highScorePoints) PlayerPrefs.SetInt("HighScore", _highScorePoints);
    }

    public void LoadHighScore()
    {
        UpdateHighScore(PlayerPrefs.GetInt("HighScore"));
    }
}
