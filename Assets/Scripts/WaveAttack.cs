using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{
    [HideInInspector] public Player player;
    private float _waveSpeed = 10;

    private void Update()
    {
        transform.Translate(Vector3.right * _waveSpeed * Time.deltaTime);
        if (transform.position.y > 15) { Destroy(gameObject); }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.AdjustHealth(-player.playerStats.GetAttackDamage());
        }

        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.Destroyed();
        }

        Boss boss;
        if (collision.gameObject.TryGetComponent<Boss>(out boss))
        {
            boss.AdjustBossHealth(-player.playerStats.GetAttackDamage());
        }
    }
}
