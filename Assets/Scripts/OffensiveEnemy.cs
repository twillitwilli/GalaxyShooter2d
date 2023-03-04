using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveEnemy : Enemy
{
    private enum EnemyState { moving, attacking, fleeing }
    private EnemyState currentState;
    [SerializeField] private GameObject _enemyLaser;
    private GameObject currentLaser;
    private float _randomStopPos, _enemyStartSpeed;
    private bool canFire, firing;

    private void Start()
    {
        currentState = EnemyState.moving;
        _randomStopPos = Random.Range(1.4f, 4.9f);
        _enemyStartSpeed = Random.Range(2.5f, 5);
    }

    public override void Update()
    {
        switch (currentState)
        {
            case EnemyState.moving:
                transform.Translate(-Vector3.up * _enemyStartSpeed * Time.deltaTime);

                if (transform.position.y <= _randomStopPos) 
                {
                    canFire = true;
                    currentState = EnemyState.attacking; 
                }
                break;

            case EnemyState.attacking:
                if (GameManager.instance.player == null) 
                {
                    firing = false;
                    currentState = EnemyState.fleeing; 
                }

                Attacking();
                break;

            case EnemyState.fleeing:
                transform.Translate(-Vector3.up * _enemyStartSpeed * Time.deltaTime);

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
        if (canFire)
        {
            firing = true;
            canFire = false;
            StartCoroutine("FireLaser");
        }
    }

    private IEnumerator FireLaser()
    {
        while (firing)
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
            if (currentLaser == null)
            {
                Vector3 spawnOffset = new Vector3(transform.position.x, transform.position.y + -0.338f, 0);
                currentLaser = Instantiate(_enemyLaser, spawnOffset, transform.rotation);
            }
        }
    }
}
