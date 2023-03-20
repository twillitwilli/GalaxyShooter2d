using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Enemy
{
    [SerializeField] private GameObject enemyBomb, enemyLaser;
    private bool _moveLeft, _fleeing, _setBombCooldown, _setLaserCooldown;
    private float _bombCooldown, _laserCooldown;

    private void Start()
    {
        float randomYPos = Random.Range(2.5f, 4.45f);
        if (Random.Range(0, 1f) > 0.5f) { _moveLeft = true; }

        if (_moveLeft) { transform.position = new Vector3(11, randomYPos, 0); }
        else { transform.position = new Vector3(-11, randomYPos, 0); }
    }

    public override void Update()
    {
        base.Update();
        if (BombCooldown()) { DropBomb(); }
        if (PlayerBehindEnemy() && LaserCooldown()) { FireLaserBackwards(); }
    }

    public override void EnemyMovement()
    {
        if (player == null || _fleeing)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            base.EnemyMovement();
        }
        else
        {
            if (_moveLeft)
            {
                transform.localEulerAngles = new Vector3(0, 0, -90);
                base.EnemyMovement();
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
                base.EnemyMovement();
            }
        }
    }

    public override void EnemyBounds()
    {
        base.EnemyBounds();
        if (transform.position.x > 11) { _moveLeft = true; }
        else if (transform.position.x < -11) { _moveLeft = false; }
    }

    private bool BombCooldown()
    {
        if (_setBombCooldown)
        {
            _bombCooldown = Random.Range(5, 9);
            _setBombCooldown = false;
        }
        if (_bombCooldown > 0) { _bombCooldown -= Time.deltaTime; }
        else if (_bombCooldown <= 0) { return true; }
        return false;
    }

    private void DropBomb()
    {
        _setBombCooldown = true;
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 0.87f, transform.position.z);
        Instantiate(enemyBomb, spawnPoint, transform.rotation);
    }

    private bool PlayerBehindEnemy()
    {
        if (transform.position.y + 0.1 <= player.transform.position.y && transform.position.y + -0.1f >= player.transform.position.y)
        {
            Debug.Log("player withing same y range");
            if (_moveLeft && transform.position.x + -0.1f < player.transform.position.x) { return true; }
            else if (!_moveLeft && transform.position.x + 0.1f > player.transform.position.x) { return true; }
        }
        return false;
    }

    private bool LaserCooldown()
    {
        if (_setLaserCooldown)
        {
            _laserCooldown = Random.Range(2, 4);
            _setLaserCooldown = false;
        }
        if (_laserCooldown > 0) { _laserCooldown -= Time.deltaTime; }
        else if (_laserCooldown <= 0) { return true; }
        return false;
    }

    private void FireLaserBackwards()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
        GameObject newLaser = Instantiate(enemyLaser, spawnPoint, transform.rotation);
        newLaser.transform.localEulerAngles = new Vector3(0, 0, 90);
    }
}
