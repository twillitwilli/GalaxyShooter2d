using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameDifficulty { easy, normal, hard }
    public GameDifficulty gameDifficulty;
    private Player _player;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }

        if (instance == this) { DontDestroyOnLoad(gameObject); }
    }

    private void Start()
    {
        StartCoroutine("GetNewPlayer");
    }

    private void Update()
    {
        if (_player == null && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            StartCoroutine("GetNewPlayer");
        }
    }

    private IEnumerator GetNewPlayer()
    {
        yield return new WaitForSeconds(1);
        _player = FindObjectOfType<Player>();
    }
}
