using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Text Notifications")]
    [SerializeField] private GameObject[] notifications;

    [Header("Score Display")]
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _highScore;
    private int _currentPoints, _highScorePoints;

    [Header("Health Display")]
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _healthImages;

    [Header("Ammo Display")]
    [SerializeField] private TMP_Text _ammoDisplay;

    private void Start()
    {
        _currentPoints = 0;
        UpdateCurrentScore(0);
        LoadHighScore();
    }

    public void ToggleNotification(bool On, int index)
    {
        if (index == 0 && On)
        {
            foreach (GameObject texts in notifications) { texts.SetActive(false); }
        }
        notifications[index].SetActive(On);
    }

    public void ResetCurrentScore()
    {
        _currentPoints = 0;
        UpdateCurrentScore(0);
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

    public void UpdateHealthDisplay(int currentHealth)
    {
        _image.sprite = _healthImages[currentHealth];
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        if (currentAmmo == 0) { ToggleNotification(true, 1); }
        else { ToggleNotification(false, 1); }
        _ammoDisplay.text = "Ammo: " + currentAmmo + "/15";
    }
}
