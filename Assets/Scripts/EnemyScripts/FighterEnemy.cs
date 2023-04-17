using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : Enemy
{
    private enum EnemyState { comeIntoScreen, movingPositions, shootingAtPlayer, destroyPowerUp, fleeing }
    private EnemyState _currentState;

    [SerializeField] private GameObject _enemyLaser;

    private GameObject _target;
    private Vector3 _targetPosition;

    private float _randomStopPos, _fireCooldown;
    private bool _setFireCooldown, setNewPosition;
    private int _positionsMoved, _shotsFired;

    private void Start()
    {
        _currentState = EnemyState.comeIntoScreen;
        _randomStopPos = Random.Range(1, 7f);
    }

    public override void Update()
    {
        if (player == null || bossIncoming) { _currentState = EnemyState.fleeing; }

        switch (_currentState)
        {
            case EnemyState.comeIntoScreen:
                transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);

                if (transform.position.y <= _randomStopPos) 
                {
                    _positionsMoved = 0;
                    setNewPosition = true;
                    _currentState = EnemyState.movingPositions; 
                }
                break;

            case EnemyState.movingPositions:
                if (setNewPosition)
                {
                    float newX = Random.Range(-14, 14);
                    float newY = Random.Range(-3.5f, 7);
                    _targetPosition = new Vector3(newX, newY, 0);
                    setNewPosition = false;
                }

                EnemyMovement();
                if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
                {
                    _positionsMoved++;

                    if (_positionsMoved == 3) 
                    {
                        _shotsFired = 0;
                        _currentState = EnemyState.shootingAtPlayer; 
                    }
                    else 
                    { 
                        setNewPosition = true;
                        ClosestPowerUp();
                        if (_target != null) { _currentState = EnemyState.destroyPowerUp; }
                    }
                }
                break;

            case EnemyState.shootingAtPlayer:
                AimAtTarget(player.transform);
                if (_shotsFired >= 3)
                {
                    _positionsMoved = 0;
                    _currentState = EnemyState.movingPositions;
                }
                else { FireLaser(); }
                break;

            case EnemyState.destroyPowerUp:
                if (_target != null)
                {
                    EnemyMovement();
                    FireLaser();
                }
                else { _currentState = EnemyState.movingPositions; }
                break;

            case EnemyState.fleeing:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                base.EnemyMovement();
                break;
        }
    }

    public override void EnemyMovement()
    {
        switch (_currentState)
        {
            case EnemyState.movingPositions:
                transform.up = transform.position - _targetPosition;
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, enemySpeed * Time.deltaTime);
                break;

            case EnemyState.destroyPowerUp:
                AimAtTarget(_target.transform);
                transform.position = Vector3.Lerp(transform.position, _target.transform.position, 0.0025f);
                break;
        }
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

    private void FireLaser()
    {
        if (_setFireCooldown)
        {
            _fireCooldown = 0.25f;
            _setFireCooldown = false;
        }
        if (_fireCooldown > 0) { _fireCooldown -= Time.deltaTime; }
        else if (_fireCooldown <= 0)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_enemyLaser, spawnPoint, transform.rotation);
            _shotsFired++;
            _setFireCooldown = true;
        }
    }
}
