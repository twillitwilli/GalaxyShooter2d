using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMirage : MonoBehaviour
{
    [HideInInspector] public Boss realBoss;
    private Player _player;

    private bool _movingLeft;
    private Vector3 _randomMovePosition;

    private int _currentHealth = 3, _maxHealth = 3;

    [SerializeField] private GameObject _bossLaser;
    private bool _setLaserCooldown;
    private float _laserCooldownTimer;

    private GivePoints _givePoints;
    private LootChance _lootChance;

    private void Start()
    {
        _player = GameManager.instance.player;
        _givePoints = GetComponent<GivePoints>();
        _lootChance = GetComponent<LootChance>();
    }

    private void Update()
    {
        transform.up = transform.position - _player.transform.position;
        Movement();
        LaserAttack();
    }

    private void Movement()
    {
        if (_movingLeft && _randomMovePosition.x >= 0) { RandomMovePosition(true); }
        else if (!_movingLeft && _randomMovePosition.x <= 0) { RandomMovePosition(false); }

        transform.position = Vector3.MoveTowards(transform.position, _randomMovePosition, 6 * Time.deltaTime * 0.75f);
        if (transform.position == _randomMovePosition)
        {
            if (_movingLeft) { _movingLeft = false; }
            else { _movingLeft = true; }
        }
    }

    private void RandomMovePosition(bool isMovingLeft)
    {
        float randomXPos = Random.Range(0.1f, 12);
        if (isMovingLeft) { randomXPos *= -1; }
        _randomMovePosition = new Vector3(randomXPos, Random.Range(0, 6.5f), 0);
    }

    private void LaserAttack()
    {
        if (_setLaserCooldown)
        {
            _laserCooldownTimer = 3;
            _setLaserCooldown = false;
        }

        if (_laserCooldownTimer > 0) { _laserCooldownTimer -= Time.deltaTime; }
        else
        {
            Instantiate(_bossLaser, transform.position, transform.rotation);
            _setLaserCooldown = true;
        }
    }

    public void AdjustCurrentHealth(int healthValue)
    {
        _currentHealth += healthValue;
        if (_currentHealth <= 0) { MirageFaded(true); }
    }

    public void AdjustMaxHealth(int newMaxHealthValue)
    {
        _maxHealth = newMaxHealthValue;
        _currentHealth = _maxHealth;
    }

    public void MirageFaded(bool killedByPlayer)
    {
        if (killedByPlayer)
        {
            realBoss.MirageFaded(this);
            _givePoints.GivePointsToPointManager();
            _lootChance.Loot(transform);
        }
        Destroy(gameObject);
    }
}
