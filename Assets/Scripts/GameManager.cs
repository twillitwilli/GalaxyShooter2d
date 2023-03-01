using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public PowerUpManager powerUpManager;
    [HideInInspector] public PointManager pointManager;
    [HideInInspector] public Player player;
    private bool setScore;

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
        StartCoroutine("GetNewPlayer");
    }

    private void Update()
    {
        if (player == null)
        {
            if (setScore)
            {
                setScore = false;
                pointManager.SaveHighScore();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                StartCoroutine("GetNewPlayer");
            }
        }
    }

    private IEnumerator GetNewPlayer()
    {
        yield return new WaitForSeconds(1);
        player = FindObjectOfType<Player>();
        powerUpManager.SetNewPlayer();
        pointManager.LoadHighScore();
        setScore = true;
    }
}
