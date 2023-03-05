using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreTextDisplay;

    private void Start()
    {
        if (GameManager.instance != null) { Destroy(GameManager.instance.gameObject); }
        highScoreTextDisplay.text = "Current High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
