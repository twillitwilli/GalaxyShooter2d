using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererEnemy : Enemy
{
    private enum EnemyState { comeIntoScreen, moving, mining, support, chasePlayer, fleeing }
    private EnemyState _currentState;

    private GameObject _target;

    [SerializeField] private GameObject _miningLaserEffect, _miningLaserShot, _hasPowerUpEffect, _powerUpShot;

    private float _randomStopPos, _fireCooldown, _supportCooldown;
    private bool _setFireCooldown, _setSupportCooldown;

    private void Start()
    {
        _currentState = EnemyState.comeIntoScreen;
        _randomStopPos = Random.Range(1.5f, 7);
    }

    public override void Update()
    {
        if (player == null || enemySpawner.IsBossSpawned()) { _currentState = EnemyState.fleeing; }
        else if (Vector2.Distance(transform.position, player.transform.position) < 3) { _currentState = EnemyState.chasePlayer; }

        switch (_currentState)
        {
            case EnemyState.comeIntoScreen:
                base.EnemyMovement();
                if (transform.position.y <= _randomStopPos) { _currentState = EnemyState.moving; }
                break;

            case EnemyState.moving:
                if (_miningLaserEffect.activeSelf) { _miningLaserEffect.SetActive(false); }
                EnemyMovement();
                EnemyBounds();
                break;

            case EnemyState.chasePlayer:
                if (_miningLaserEffect.activeSelf) { _miningLaserEffect.SetActive(false); }
                ChasePlayer();
                break;

            case EnemyState.mining:
                if (!_miningLaserEffect.activeSelf)
                {
                    _miningLaserEffect.SetActive(true);
                    _setFireCooldown = true;
                }
                FireGathererLaserShot();
                if (_target == null || MeteorRange() > 10) { _currentState = EnemyState.moving; }
                else { EnemyMovement(); }
                break;

            case EnemyState.support:
                if (_miningLaserEffect.activeSelf) { _miningLaserEffect.SetActive(false); }
                HasPowerUp();
                break;

            case EnemyState.fleeing:
                if (_miningLaserEffect.activeSelf) { _miningLaserEffect.SetActive(false); }
                transform.localEulerAngles = new Vector3(0, 0, 0);
                base.EnemyMovement();
                break;
        }
    }

    private void ChasePlayer()
    {
        AimAtTarget(player.transform);
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.005f);
        if (Vector2.Distance(transform.position, player.transform.position) > 5) { _currentState = EnemyState.moving; }
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
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, 0.75f * Time.deltaTime);
            if (MeteorRange() < 6) { _currentState = EnemyState.mining; }
        }
    }

    private GameObject ClosestMeteor()
    {
        GameObject closetObj = null;
        Vector2 positionPoint = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(positionPoint, 15);
        if (colliders.Length > 0)
        {
            Meteor meteor;
            float closestDistance = 100;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.TryGetComponent<Meteor>(out meteor) && meteor.gatherer == null)
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
        if (closetObj != null) { closetObj.GetComponent<Meteor>().gatherer = this; }
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
            _fireCooldown = Random.Range(2, 3.5f);
            _setFireCooldown = false;
        }

        if (_fireCooldown <= 0)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + -0.71f, transform.position.z);
            GameObject newLaserShot = Instantiate(_miningLaserShot, spawnPoint, transform.rotation);
            newLaserShot.GetComponent<GathererLaser>().gatherer = this;
            _setFireCooldown = true;
        }
        else { _fireCooldown -= Time.deltaTime; }
    }

    public void ObtainedPowerUp()
    {
        _setSupportCooldown = true;
        _currentState = EnemyState.support;
        _hasPowerUpEffect.SetActive(true);
    }

    private void HasPowerUp()
    {
        if (SupportCooldown())
        {
            Vector2 point = new Vector2(transform.position.x, transform.position.y);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point, 8);
            Enemy enemy;
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.TryGetComponent<Enemy>(out enemy) && enemy != this && !enemy.IsShieldActive())
                    {
                        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + -0.75f, transform.position.z);
                        GameObject newPowerUpShot = Instantiate(_powerUpShot, spawnPoint, transform.rotation);
                        newPowerUpShot.GetComponent<EnemyPowerUp>()._target = enemy;
                        _hasPowerUpEffect.SetActive(false);
                    }
                }
            }
            _currentState = EnemyState.moving;
        }
    }

    private bool SupportCooldown()
    {
        if (_setSupportCooldown)
        {
            _supportCooldown = 5;
            _setSupportCooldown = false;
        }
        if (_supportCooldown > 0) { _supportCooldown -= Time.deltaTime; }
        else if (_supportCooldown <= 0) { return true; }
        return false;
    }
}
