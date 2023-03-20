using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : Enemy
{
    private enum EnemyState { comeIntoScreen, chasePlayer, destroyPowerUp, fleeing }
    private EnemyState _currentState;
    private GameObject _target;
    [SerializeField] private GameObject _enemyLaser;
    private float _randomStopPos, _fireCooldown;
    private bool _setFireCooldown;

    private void Start()
    {
        _currentState = EnemyState.comeIntoScreen;
        _randomStopPos = Random.Range(1, 4.5f);
    }

    public override void Update()
    {
        if (player == null || bossIncoming) { _currentState = EnemyState.fleeing; }
        else if (Vector2.Distance(transform.position, player.transform.position) < 3) { _currentState = EnemyState.chasePlayer; }

        switch (_currentState)
        {
            case EnemyState.comeIntoScreen:
                transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
                if (transform.position.y <= _randomStopPos) { _currentState = EnemyState.chasePlayer; }
                break;

            case EnemyState.chasePlayer:
                AimAtTarget(player.transform);
                if (!PlayerWithinRange()) { ChasePlayer(); }
                FireLaser();
                break;

            case EnemyState.destroyPowerUp:
                if (_target != null)
                {
                    EnemyMovement();
                    FireLaser();
                }
                else { _currentState = EnemyState.chasePlayer; }
                break;

            case EnemyState.fleeing:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
                break;
        }

        if (_target == null) 
        { 
            if (_currentState != EnemyState.chasePlayer) { _currentState = EnemyState.chasePlayer; }
            ClosestPowerUp(); 
        }
        else { _currentState = EnemyState.destroyPowerUp; }
    }

    public override void EnemyMovement()
    {
        AimAtTarget(_target.transform);
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, 0.0025f);
    }

    private void ClosestPowerUp()
    {
        GameObject closetObj = null;
        Vector2 positionPoint = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(positionPoint, 5);
        if (colliders.Length > 0)
        {
            PowerUp powerUp;
            float closestDistance = 100;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.TryGetComponent<PowerUp>(out powerUp))
                {
                    float distanceCheck = Vector2.Distance(transform.position, powerUp.transform.position);
                    if (distanceCheck < closestDistance)
                    {
                        closestDistance = distanceCheck;
                        closetObj = colliders[i].gameObject;
                    }
                }
            }
        }
        if (closetObj != null) { _target = closetObj; }
    }

    private bool PlayerWithinRange()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < _randomStopPos) { return true; }
        else return false;
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.005f);
    }

    private void FireLaser()
    {
        if (_setFireCooldown)
        {
            _fireCooldown = Random.Range(1.5f, 3);
            _setFireCooldown = false;
        }
        if (_fireCooldown > 0) { _fireCooldown -= Time.deltaTime; }
        else if (_fireCooldown <= 0)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_enemyLaser, spawnPoint, transform.rotation);
            _setFireCooldown = true;
        }
    }
}
