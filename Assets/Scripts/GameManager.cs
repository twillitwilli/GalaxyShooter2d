using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameDifficulty { easy, normal, hard }
    public GameDifficulty gameDifficulty;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
        if (instance == this) { DontDestroyOnLoad(gameObject); }
    }
}
