using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    [HideInInspector] public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor)) { meteor.Destroyed(); }

        Enemy enemy;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy)) { enemy.AdjustHealth(-player.playerStats.GetAttackDamage() * 2); }

        Boss boss;
        if (collision.gameObject.TryGetComponent<Boss>(out boss)) { boss.AdjustBossHealth(-player.playerStats.GetAttackDamage() * 2); }
    }
}
