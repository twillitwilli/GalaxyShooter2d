using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _meteor;
    [SerializeField] private Transform _spawnParent;
    [HideInInspector] public bool disableSpawner;

    private void Start()
    {
        StartCoroutine("SpawnMeteors");
    }

    private IEnumerator SpawnMeteors()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            Vector3 spawnPoint = new Vector3(Random.Range(-9.4f, 9.4f), 6.9f, 0);
            GameObject newMeteor = Instantiate(_meteor, spawnPoint, transform.rotation);
            newMeteor.transform.SetParent(_spawnParent);
        }
    }
}
