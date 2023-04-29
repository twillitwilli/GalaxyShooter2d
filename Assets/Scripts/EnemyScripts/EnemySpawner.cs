using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _enemyParent;

    [HideInInspector] public bool disableSpawner, updateWave;

    private GameObject spawnedBoss;

    private int _currentlySpawnedEnemies, _totalEnemiesKilled, _currentEnemyWave = 1, _currentLevel = 0, _waveDisplayMultiplier = 0;

    public IEnumerator SpawnEnemies()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(1, 2.5f));
            if (updateWave) { WaveDisplay(); }
            if (_currentlySpawnedEnemies < 10 + _currentLevel && spawnedBoss == null) { SpawnEnemy(); }
        }
    }

    private int GetRandomEnemy()
    {
        switch (_currentEnemyWave)
        {
            case 2:
                float offensiveEnemySpawnChance = Random.Range(0, 100);
                if (offensiveEnemySpawnChance > 65) { return 1; }
                break;

            case 3:
                float bomberEnemySpawnChance = Random.Range(0, 100);
                if (bomberEnemySpawnChance > 80) { return 2; }
                else if (bomberEnemySpawnChance < 80 && bomberEnemySpawnChance > 45) { return 1; }
                break;

            case 4:
                float fighterEnemySpawnChance = Random.Range(0, 100);
                if (fighterEnemySpawnChance > 70) { return 3; }
                else if (fighterEnemySpawnChance < 70 && fighterEnemySpawnChance > 55) { return 2; }
                else if (fighterEnemySpawnChance < 55 && fighterEnemySpawnChance > 30) { return 1; }
                break;

            case 5:
                return 4;
        }
        return 0;
    }

    private void SpawnEnemy()
    {
        _currentlySpawnedEnemies++;

        Vector3 spawnPoint = new Vector3(Random.Range(-14f, 14f), 10, 0);
        GameObject newEnemy = Instantiate(_enemies[GetRandomEnemy()], spawnPoint, transform.rotation);
        newEnemy.transform.SetParent(_enemyParent);

        if (_currentEnemyWave != 5) { newEnemy.GetComponent<Enemy>().enemySpawner = this; }
        else 
        {
            newEnemy.GetComponent<Boss>().enemySpawner = this;
            spawnedBoss = newEnemy;
            disableSpawner = true;
        }
    }

    private void WaveDisplay()
    {
        updateWave = false;
        if (_currentEnemyWave != 5) { GameManager.instance.displayManager.WaveUpdateNotification("Wave " + (_currentEnemyWave + _waveDisplayMultiplier)); }
        else { GameManager.instance.displayManager.WaveUpdateNotification("Boss Incoming"); }
        
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void IncreaseCurrentLevel()
    {
        _currentLevel++;
        _totalEnemiesKilled = 0;
        _waveDisplayMultiplier = _currentLevel * 5;
        _currentEnemyWave = 1;
        updateWave = true;
        StartCoroutine("StartNextLevel");
    }

    private IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(8);
        disableSpawner = false;
        StartCoroutine("SpawnEnemies");
    }

    public bool IsBossSpawned()
    {
        if (spawnedBoss == null) { return false; }
        else return true;
    }

    public void EnemyDestroyed(bool destroyedByPlayer)
    {
        _currentlySpawnedEnemies--;
        if (destroyedByPlayer)
        {
            _totalEnemiesKilled++;
            switch (_totalEnemiesKilled)
            {
                case 12:
                    _currentEnemyWave = 2;
                    updateWave = true;
                    break;

                case 35:
                    _currentEnemyWave = 3;
                    updateWave = true;
                    break;

                case 55:
                    _currentEnemyWave = 4;
                    updateWave = true;
                    break;
                case 80:
                    _currentEnemyWave = 5;
                    updateWave = true;
                    break;
            }
        }
    }
}
