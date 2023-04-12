using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Enemy
{
    [SerializeField] private GameObject _enemyBomb, _enemyFireball;
    private bool _moveLeft, _fleeing, _setBombCooldown, _setFireballCooldown;
    private float _bombCooldown, _fireballCooldown;

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
        if (FireballCooldown() && PlayerBehindEnemy()) { ShootFireball(); }
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
        Instantiate(_enemyBomb, spawnPoint, transform.rotation);
    }

    private bool PlayerBehindEnemy()
    {
        float yPosDifference = transform.position.y - player.transform.position.y;
        if (yPosDifference <= 1.5f && yPosDifference >= -1.5f)
        {
            if (_moveLeft && transform.position.x + -0.1f < player.transform.position.x) { return true; }
            else if (!_moveLeft && transform.position.x + 0.1f > player.transform.position.x) { return true; }
        }
        return false;
    }

    private bool FireballCooldown()
    {
        if (_setFireballCooldown)
        {
            _fireballCooldown = Random.Range(2, 4);
            _setFireballCooldown = false;
        }
        if (_fireballCooldown > 0) { _fireballCooldown -= Time.deltaTime; }
        else if (_fireballCooldown <= 0) { return true; }
        return false;
    }

    private void ShootFireball()
    {
        _setFireballCooldown = true;
        Instantiate(_enemyFireball, transform.position, transform.rotation);
    }
}
