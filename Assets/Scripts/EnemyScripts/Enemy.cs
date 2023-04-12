using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public EnemySpawner enemySpawner;
    private GivePoints _givePoints;
    private LootChance _lootChance;

    [SerializeField] private GameObject _enemyExplosion, _shield;

    [SerializeField] private int _health;
    private int _maxHealth;
    public float enemySpeed;

    [HideInInspector] public bool bossIncoming;

    private void Awake()
    {
        player = GameManager.instance.player;
        _givePoints = GetComponent<GivePoints>();
        _lootChance = GetComponent<LootChance>();
        _maxHealth = _health;
        enemySpeed = Random.Range(enemySpeed - 1, enemySpeed + 1);
    }

    public virtual void Update()
    {
        EnemyMovement();
        EnemyBounds();
    }

    public virtual void EnemyMovement()
    {
        transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
    }  

    public virtual void EnemyBounds()
    {
        if (transform.position.y < -9.3f)
        {
            enemySpawner.EnemyDestroyed(false);
            Destroy(gameObject);
        }
    }

    public virtual void AimAtTarget(Transform target)
    {
        transform.up = transform.position - target.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
            AdjustHealth(-2);
        }
    }

    public void AdjustHealth(int healthValue)
    {
        if (!_shield.activeSelf)
        {
            _health += healthValue;
            if (_health > _maxHealth) { _health = _maxHealth; }
            else if (_health <= 0) { EnemyDestroyed(); }
        }
        else { _shield.SetActive(false); }
    }

    public bool IsShieldActive()
    {
        if (_shield.activeSelf) { return true; }
        else { return false; }
    }

    public void ActivateShield()
    {
        _shield.SetActive(true);
    }

    private void EnemyDestroyed()
    {
        _givePoints.GivePointsToPointManager();
        _lootChance.Loot(transform);
        enemySpawner.EnemyDestroyed(true);
        enemySpawner.RemoveTrackedEnemy(false, gameObject);
        GameManager.instance.cameraController.ShakeCamera();
        Instantiate(_enemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
