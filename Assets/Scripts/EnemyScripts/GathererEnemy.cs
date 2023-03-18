using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererEnemy : Enemy
{
    private enum EnemyState { comeIntoScreen, moving, mining, collecting, fleeing }
    private EnemyState _currentState;
    private GameObject _target;
    [SerializeField] private GameObject _miningLaserEffect, _miningLaserShot;
    private float _randomStopPos, fireCooldown;
    private bool _setFireCooldown;

    private void Start()
    {
        _currentState = EnemyState.comeIntoScreen;
        _randomStopPos = Random.Range(0, 3);
    }

    public override void Update()
    {
        switch (_currentState)
        {
            case EnemyState.comeIntoScreen:
                transform.Translate(-Vector3.up * enemySpeed * Time.deltaTime);
                if (transform.position.y <= _randomStopPos) { _currentState = EnemyState.moving; }
                break;

            case EnemyState.moving:
                if (_miningLaserEffect.activeSelf) { _miningLaserEffect.SetActive(false); }
                EnemyMovement();
                EnemyBounds();
                break;

            case EnemyState.mining:
                if (!_miningLaserEffect.activeSelf) 
                { 
                    _miningLaserEffect.SetActive(true);
                    _setFireCooldown = true;
                }
                FireGathererLaserShot();
                if (_target == null || MeteorRange() > 3) { _currentState = EnemyState.moving; }
                else { EnemyMovement(); }
                break;
        }
    }

    public override void EnemyMovement()
    {
        if (_target == null)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            base.EnemyMovement();
            _target = ClosestMeteor();
        }
        else
        {
            AimAtTarget(_target.transform);
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, 0.0001f);
            if (MeteorRange() < 1) { _currentState = EnemyState.mining; }
        }
    }

    private GameObject ClosestMeteor()
    {
        GameObject closetObj = null;
        Vector2 positionPoint = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(positionPoint, 5);
        if (colliders.Length > 0)
        {
            Meteor meteor;
            float closestDistance = 100;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.TryGetComponent<Meteor>(out meteor))
                {
                    float distanceCheck = Vector2.Distance(transform.position, meteor.transform.position);
                    if (distanceCheck < closestDistance)
                    {
                        closestDistance = distanceCheck;
                        closetObj = colliders[i].gameObject;
                    }
                }
            }
        }
        return closetObj;
    }

    private float MeteorRange()
    {
        return Vector2.Distance(transform.position, _target.transform.position);
    }

    private void FireGathererLaserShot()
    {
        if (_setFireCooldown)
        {
            fireCooldown = Random.Range(2, 3.5f);
            _setFireCooldown = false;
        }

        if (fireCooldown <= 0)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + -0.71f, transform.position.z);
            Instantiate(_miningLaserShot, spawnPoint, transform.rotation);
            _setFireCooldown = true;
        }
        else { fireCooldown -= Time.deltaTime; }
    }
}
