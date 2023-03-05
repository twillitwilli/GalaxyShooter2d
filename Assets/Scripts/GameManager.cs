using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public PowerUpManager powerUpManager;
    [HideInInspector] public PointManager pointManager;
    [HideInInspector] public HealthDisplayManager healhDisplayManager;
    [HideInInspector] public Player player;
    private bool _setScore;
    [SerializeField] private GameObject _gameOverScreen;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }

        if (instance == this) { DontDestroyOnLoad(gameObject); }
    }

    private void Start()
    {
        powerUpManager = GetComponent<PowerUpManager>();
        pointManager = GetComponent<PointManager>();
        healhDisplayManager = GetComponent<HealthDisplayManager>();
        StartCoroutine("GetNewPlayer");
    }

    private void Update()
    {
        if (player == null)
        {
            if (_setScore)
            {
                _setScore = false;
                pointManager.SaveHighScore();
                _gameOverScreen.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
                pointManager.ResetCurrentScore();
                _gameOverScreen.SetActive(false);
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
        pointManager.LoadHighScore();
        _setScore = true;
    }
}
