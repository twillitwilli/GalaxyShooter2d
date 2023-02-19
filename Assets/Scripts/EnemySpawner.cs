using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int totalEnemiesSpawned, totalEnemiesKilled;
    private float spawnRate;
    private bool setSpawnCooldown;

    private void Update()
    {
        if (CanSpawnEnemy())
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-9.3f, 9.3f), 9, 0);
            GameObject newEnemy = Instantiate(enemy, spawnPoint, transform.rotation);
            newEnemy.GetComponent<Enemy>().enemySpawner = this;
            totalEnemiesSpawned++;
            setSpawnCooldown = true;
        }
    }

    private bool CanSpawnEnemy()
    {
        if (setSpawnCooldown)
        {
            spawnRate = Random.Range(1, 3);
            setSpawnCooldown = false;
        }
        if (spawnRate > 0) { spawnRate -= Time.deltaTime; }
        else return true;
        return false;
    }
}
