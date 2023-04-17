using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _meteor;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Transform _spawnParent;
    [HideInInspector] public bool disableSpawner, enemySpawnerActive;
    [HideInInspector] public int meteorsGathered, meteorsDestroyed;

    private void Start()
    {
        StartCoroutine("SpawnMeteors");
    }

    private IEnumerator SpawnMeteors()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 1f));
            Vector3 spawnPoint = new Vector3(Random.Range(-15, 15f), 10, 0);
            GameObject newMeteor = Instantiate(_meteor, spawnPoint, transform.rotation);
            newMeteor.transform.SetParent(_spawnParent);
            newMeteor.GetComponentInChildren<Meteor>().spawner = this;
        }
    }

    public void TurnOnEnemySpawns()
    {
        _enemySpawner.updateWave = true;
        enemySpawnerActive = true;
        _enemySpawner.StartCoroutine("SpawnEnemies");
    }
}
