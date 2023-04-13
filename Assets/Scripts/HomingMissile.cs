using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [HideInInspector] public Player player;
    [SerializeField] private GameObject missileImpactExplosion;
    private bool _targetLocked;
    private Transform _target;

    private void Update()
    {
        if (_target == null) { _targetLocked = false; }
        if (_targetLocked) 
        { 
            transform.up = _target.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, 6 * Time.deltaTime);
        }
        else { transform.Translate(Vector3.up * 2.5f * Time.deltaTime); }
        if (transform.position.y >= 6) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        Enemy enemy;
        Boss boss;

        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.Destroyed();
            MissileImpact();
        }
        else if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.AdjustHealth(-player.playerStats.GetAttackDamage() * 3);
            MissileImpact();
        }
        else if (collision.gameObject.TryGetComponent<Boss>(out boss))
        {
            boss.AdjustBossHealth(-player.playerStats.GetAttackDamage() * 3);
            MissileImpact();
        }
    }

    private void MissileImpact()
    {
        GameObject newExplosion = Instantiate(missileImpactExplosion, transform.position, transform.rotation);
        newExplosion.GetComponent<MissileExplosion>().player = player;
        Destroy(gameObject);
    }

    public bool HasTarget()
    {
        return _targetLocked;
    }

    public void NewTargetFound(Transform target)
    {
        _target = target;
        _targetLocked = true;
    }
}
