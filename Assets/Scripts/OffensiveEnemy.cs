using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveEnemy : Enemy
{
    [SerializeField] private GameObject _enemyLaser;
    private GameObject currentLaser;
    private float _randomStopPos, _enemyStartSpeed;
    private bool canFire;

    private void Start()
    {
        _randomStopPos = Random.Range(1.4f, 4.9f);
        _enemyStartSpeed = Random.Range(2.5f, 5);
    }

    public override void Update()
    {
        if (transform.position.y > _randomStopPos) { transform.Translate(-Vector3.up * _enemyStartSpeed * Time.deltaTime); }
        else { canFire = true; }

        if (canFire)
        {
            canFire = false;
            StartCoroutine("FireLaser");
        }
    }

    private IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        if (currentLaser == null)
        {
            Vector3 spawnOffset = new Vector3(transform.position.x, transform.position.y + -0.699f, 0);
            currentLaser = Instantiate(_enemyLaser, spawnOffset, transform.rotation);
        }
    }
}
