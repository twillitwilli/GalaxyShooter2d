using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;
    [SerializeField] private float health, enemySpeed;
    [SerializeField] [Range(1, 100)] private float lootChance;
    private float maxHealth;
    public GameObject enemyExplosion;

    private void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
        if (transform.position.y < -9.3f) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-26);
            AdjustHealth(player.playerStats.GetAttackDamage() * 2);
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
        int randomSpawnChance = Random.Range(0, 100);
        if (randomSpawnChance < lootChance) { GameManager.instance.powerUpManager.SpawnPowerUp(transform); }
        enemySpawner.totalEnemiesKilled++;
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
