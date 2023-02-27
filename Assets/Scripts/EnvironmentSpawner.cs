using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    [SerializeField] private Transform spawnParent;
    [HideInInspector] public bool disableSpawner;

    private void Start()
    {
        StartCoroutine("SpawnMeteors");
    }

    private IEnumerator SpawnMeteors()
    {
        while (!disableSpawner)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
            Vector3 spawnPoint = new Vector3(Random.Range(-9.4f, 9.4f), 6.9f, 0);
            GameObject newMeteor = Instantiate(meteor, spawnPoint, transform.rotation);
            newMeteor.transform.SetParent(spawnParent);
        }
    }
}
