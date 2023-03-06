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
        if (Input.GetKeyDown(KeyCode.Return)) { LoadGame(); }
        else if (Input.GetKeyDown(KeyCode.Escape)) { ExitGame(); }
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
