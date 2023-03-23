using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum BossState { idle, mirage }
    private BossState _currentState;
    [HideInInspector] public EnemySpawner enemySpawner;
    [SerializeField] private GameObject _mirage;
    [SerializeField] private GameObject _bossLaser, _bomb;
    private List<GameObject> _enemies = new List<GameObject>();
    private int _maxHealth, _currentHealth;

    private void Start()
    {
        _enemies.Add(gameObject);
        _maxHealth = 50 + (10 * enemySpawner.GetCurrentLevel());
        _currentHealth = _maxHealth;
        GameManager.instance.displayManager.BossHealthDisplay(true);
        GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth);
        _currentState = BossState.idle;
        transform.position = new Vector3(0, 6, 0);
    }

    public void StartBossFight()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    public void AdjustBossHealth(int healthValue)
    {
        _currentHealth += healthValue;
        if (_currentHealth <= 0) { BossKilled(); }
        else { GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth); }
    }

    public void ActivateMirageState()
    {
        for (int i = 0; i < 2; i++) { SpawnMirages(); }
    }

    private void SpawnMirages()
    {
        GameObject mirage = Instantiate(_mirage, transform.position, transform.rotation);
        _enemies.Add(mirage);
    }

    private void BossKilled()
    {
        GameManager.instance.displayManager.BossHealthDisplay(false);
    }
}
