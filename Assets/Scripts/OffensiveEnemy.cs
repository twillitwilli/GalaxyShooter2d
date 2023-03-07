using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveEnemy : Enemy
{
    private enum EnemyState { _moving, _attacking, _fleeing }
    private EnemyState _currentState;
    [SerializeField] private GameObject _enemyLaser;
    private GameObject _currentLaser;
    private float _randomStopPos, _enemyStartSpeed;
    private bool _canFire, _firing;

    private void Start()
    {
        _currentState = EnemyState._moving;
        _randomStopPos = Random.Range(1.4f, 4.9f);
        _enemyStartSpeed = Random.Range(2.5f, 5);
    }


    public override void Update()
    {
        switch (_currentState)
        {
            case EnemyState._moving:
                transform.Translate(-Vector3.up * _enemyStartSpeed * Time.deltaTime);

                if (transform.position.y <= _randomStopPos) 
                {
                    _canFire = true;
                    _currentState = EnemyState._attacking; 
                }
                break;

            case EnemyState._attacking:
                if (GameManager.instance.player == null) 
                {
                    _firing = false;
                    _currentState = EnemyState._fleeing; 
                }

                Attacking();
                break;

            case EnemyState._fleeing:
                transform.Translate(-Vector3.up * (_enemyStartSpeed + 2) * Time.deltaTime);

                if (transform.position.y < -9.3f)
                {
                    enemySpawner.EnemyDestroyed(false);
                    Destroy(gameObject);
                }
                break;
        }
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
                Vector3 spawnOffset = new Vector3(transform.position.x, transform.position.y + -0.338f, 0);
                _currentLaser = Instantiate(_enemyLaser, spawnOffset, transform.rotation);
            }
        }
    }
}
