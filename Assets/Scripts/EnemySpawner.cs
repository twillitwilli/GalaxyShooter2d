using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _enemyParent;
    public int enemiesSpawned, totalEnemiesKilled;
    [HideInInspector] public bool disableSpawner;

    private void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    private IEnumerator SpawnEnemies()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(1, 2.5f));
            Vector3 spawnPoint = new Vector3(Random.Range(-9.3f, 9.3f), 9, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnPoint, transform.rotation);
            newEnemy.GetComponent<Enemy>().enemySpawner = this;
            newEnemy.transform.SetParent(_enemyParent);
            enemiesSpawned++;
        }
    }
}
