using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreTextDisplay;

    private void Start()
    {
        if (GameManager.instance != null) { Destroy(GameManager.instance.gameObject); }
        _highScoreTextDisplay.text = "Current High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
