using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _enemyParent;
    [HideInInspector] public bool disableSpawner;
    private float _currentlySpawnedEnemies, _totalEnemiesKilled;

    private void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    private IEnumerator SpawnEnemies()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(1, 2.5f));
            if (_currentlySpawnedEnemies < 12)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-9.3f, 9.3f), 9, 0);
                GameObject newEnemy = Instantiate(_enemies[GetRandomEnemy()], spawnPoint, transform.rotation);
                newEnemy.GetComponent<Enemy>().enemySpawner = this;
                newEnemy.transform.SetParent(_enemyParent);
                _currentlySpawnedEnemies++;
            }
        }
    }

    private int GetRandomEnemy()
    {
        float aggresiveEnemySpawnChance = Random.Range(0, 100);
        if (_totalEnemiesKilled > 5 && aggresiveEnemySpawnChance > 45) { return 1; }
        return 0;
    }

    public void EnemyDestroyed(bool destroyedByPlayer)
    {
        if (destroyedByPlayer) { _totalEnemiesKilled++; }
        _currentlySpawnedEnemies--;
    }
}
