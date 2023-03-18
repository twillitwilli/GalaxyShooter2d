using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveEnemy : Enemy
{
    private Player _player;
    private enum EnemyState { moving, attacking, fleeing }
    private EnemyState _currentState;
    [SerializeField] private GameObject _enemyLaser;
    private GameObject _currentLaser;
    private float _randomStopPos;
    private bool _canFire, _firing;

    private void Start()
    {
        _player = GameManager.instance.player;
        _currentState = EnemyState.moving;
        _randomStopPos = Random.Range(1.4f, 4.9f);
    }


    public override void Update()
    {
        switch (_currentState)
        {
            case EnemyState.moving:
                EnemyMovement();
                break;

            case EnemyState.attacking:
                if (_player == null) 
                {
                    _firing = false;
                    _currentState = EnemyState.fleeing; 
                }

                Attacking();
                break;

            case EnemyState.fleeing:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.Translate(-Vector3.up * (enemySpeed + 2) * Time.deltaTime);

                if (transform.position.y < -9.3f)
                {
                    enemySpawner.EnemyDestroyed(false);
                    Destroy(gameObject);
                }
                break;
        }
    }

    public override void EnemyMovement()
    {
        base.EnemyMovement();
        if (transform.position.y <= _randomStopPos)
        {
            _canFire = true;
            _currentState = EnemyState.attacking;
        }
    }

    private void LateUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.attacking:
                if (_player != null) { AimAtPlayer(); }
                break;
        }
    }

    private void AimAtPlayer()
    {
        transform.up = transform.position - _player.transform.position;
    }

    public virtual void Attacking()
    {
        if (_canFire)
        {
            _firing = true;
            _canFire = false;
            StartCoroutine("FireLaser");
        }
    }

    private IEnumerator FireLaser()
    {
        while (_firing)
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
            if (_currentLaser == null)
            {
                Vector3 spawnOffset = new Vector3(transform.position.x, transform.position.y + -0.5f, 0);
                _currentLaser = Instantiate(_enemyLaser, spawnOffset, transform.rotation);
            }
        }
    }
}