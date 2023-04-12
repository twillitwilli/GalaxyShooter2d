using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveEnemy : Enemy
{
    private enum EnemyState { moving, attacking, dodging, fleeing }
    private EnemyState _currentState;
    [SerializeField] private GameObject _enemyLaser;
    private GameObject _currentLaser;
    private float _randomStopPos;
    private bool _canFire, _firing, _moveLeft;

    private void Start()
    {
        _currentState = EnemyState.moving;
        _randomStopPos = Random.Range(1.5f, 4.45f);
    }


    public override void Update()
    {
        if (player == null || bossIncoming) { _currentState = EnemyState.fleeing; }
        switch (_currentState)
        {
            case EnemyState.moving:
                EnemyMovement();
                break;

            case EnemyState.attacking:
                Attacking();
                if (player != null) { AimAtTarget(player.transform); }
                break;

            case EnemyState.dodging:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                DodgeMovement();
                break;

            case EnemyState.fleeing:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                base.EnemyMovement();

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

    private void DodgeMovement()
    {
        if (_moveLeft) { transform.Translate(-Vector3.right * enemySpeed * Time.deltaTime); }
        else { transform.Translate(Vector3.right * enemySpeed * Time.deltaTime); }
        DodgeBounds();
    }

    private void DodgeBounds()
    {
        if (transform.position.x < -9.5f) 
        {
            transform.position = new Vector3(-9.5f, transform.position.y, transform.position.z);
            _moveLeft = false; 
        }
        else if (transform.position.x > 9.5f) 
        {
            transform.position = new Vector3(9.5f, transform.position.y, transform.position.z);
            _moveLeft = true; 
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
                Vector3 spawnOffset = new Vector3(transform.position.x, transform.position.y + -0.5f, 0);
                _currentLaser = Instantiate(_enemyLaser, spawnOffset, transform.rotation);
            }
        }
    }

    public void DodgeChance()
    {
        float dodgeChance = Random.Range(0, 100);
        if (dodgeChance > 60) 
        { 
            _currentState = EnemyState.dodging;
            StartCoroutine("EndDodgeMovement");
        }
    }

    private IEnumerator EndDodgeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        _currentState = EnemyState.attacking;
    }
}
