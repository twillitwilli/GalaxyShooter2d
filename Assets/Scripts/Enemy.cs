using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;
    [HideInInspector] public bool enemyEscaped;
    public float enemySpeed;

    void Update()
    {
        transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
        if (transform.position.y < -9.3f)
        {
            enemyEscaped = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!enemyEscaped) { enemySpawner.totalEnemiesKilled++; }
    }
}
