using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _godMode, _resetHighScore;
    public static GameManager instance;
    [HideInInspector] public CameraController cameraController;
    [HideInInspector] public PowerUpManager powerUpManager;
    [HideInInspector] public UIManager displayManager;
    [HideInInspector] public Player player;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }

        if (instance == this) { DontDestroyOnLoad(gameObject); }
    }

    private void Start()
    {
        if (_resetHighScore) { PlayerPrefs.SetInt("HighScore", 0); }
        powerUpManager = GetComponent<PowerUpManager>();
        displayManager = GetComponent<UIManager>();
        StartCoroutine("GetNewPlayer");
    }

    private void Update()
    {
        if (player == null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
                displayManager.ResetCurrentScore();
                displayManager.ToggleNotification(false, 0);
                StartCoroutine("GetNewPlayer");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private IEnumerator GetNewPlayer()
    {
        yield return new WaitForSeconds(1);
        player = FindObjectOfType<Player>();
        powerUpManager.SetNewPlayer();
        displayManager.LoadHighScore();
        displayManager.UpdateFuelDisplay(player.playerStats.ThrusterFuel());
    }

    public bool IsGodModeActive()
    {
        return _godMode;
    }
}
