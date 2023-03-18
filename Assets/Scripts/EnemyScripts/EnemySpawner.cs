using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _enemyParent;
    [HideInInspector] public bool disableSpawner, updateWave;
    private float _totalEnemiesKilled;
    private int _currentEnemyWave =1;

    public IEnumerator SpawnEnemies()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(1, 2.5f));
            if (updateWave) { WaveDisplay(); }
            Vector3 spawnPoint = new Vector3(Random.Range(-9.3f, 9.3f), 9, 0);
            GameObject newEnemy = Instantiate(_enemies[GetRandomEnemy()], spawnPoint, transform.rotation);
            newEnemy.GetComponent<Enemy>().enemySpawner = this;
            newEnemy.transform.SetParent(_enemyParent);
        }
    }

    private int GetRandomEnemy()
    {
        switch (_currentEnemyWave)
        {
            case 2:
                float aggresiveEnemySpawnChance = Random.Range(0, 100);
                if (aggresiveEnemySpawnChance > 45) { return 1; }
                break;
        }
        return 0;
    }

    private void WaveDisplay()
    {
        updateWave = false;
        GameManager.instance.displayManager.WaveUpdateNotification("Wave " + _currentEnemyWave);
    }

    public void EnemyDestroyed(bool destroyedByPlayer)
    {
        if (destroyedByPlayer)
        {
            _totalEnemiesKilled++;
            switch (_totalEnemiesKilled)
            {
                case 12:
                    _currentEnemyWave = 2;
                    updateWave = true;
                    break;

                case 22:
                    _currentEnemyWave = 3;
                    updateWave = true;
                    break;

                case 30:
                    _currentEnemyWave = 4;
                    updateWave = true;
                    break;
            }
        }
    }
}
