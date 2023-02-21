using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;
    [SerializeField] private float health, enemySpeed;
    private float maxHealth;
    public GameObject enemyExplosion;

    private void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
        if (transform.position.y < -9.3f)
        {
            Destroy(gameObject);
        }
    }

    public void AdjustHealth(float healthValue)
    {
        health += healthValue;
        if (health > maxHealth) { health = maxHealth; }
        else if (health < 0) { EnemyDestroyed(); }
    }

    private void EnemyDestroyed()
    {
        enemySpawner.totalEnemiesKilled++;
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
