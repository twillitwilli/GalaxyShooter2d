using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;
    [SerializeField] private float _health;
    private float _maxHealth, _enemySpeed;
    [SerializeField] private GameObject enemyExplosion;
    private GivePoints _givePoints;
    private LootChance _lootChance;

    private void Awake()
    {
        _givePoints = GetComponent<GivePoints>();
        _lootChance = GetComponent<LootChance>();
    }

    private void Start()
    {
        _maxHealth = _health;
        _enemySpeed = Random.Range(2.5f, 5);
    }

    public virtual void Update()
    {
        transform.Translate(-Vector3.up * _enemySpeed * Time.deltaTime);
        if (transform.position.y < -9.3f) 
        {
            enemySpawner.EnemyDestroyed(false);
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
            AdjustHealth(-player.playerStats.GetAttackDamage() * 2);
        }
    }

    public void AdjustHealth(float healthValue)
    {
        _health += healthValue;
        if (_health > _maxHealth) { _health = _maxHealth; }
        else if (_health < 0) { EnemyDestroyed(); }
    }

    private void EnemyDestroyed()
    {
        _givePoints.GivePointsToPointManager();
        _lootChance.Loot(transform);
        enemySpawner.EnemyDestroyed(true);
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
