using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;
    public float enemySpeed;
    public GameObject enemyExplosion;

    void Update()
    {
        transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
        if (transform.position.y < -9.3f)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDestroyed()
    {
        enemySpawner.totalEnemiesKilled++;
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
