using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public Player player;
    private float _laserSpeed = 10;

    private void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy;
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.AdjustHealth(-player.playerStats.GetAttackDamage());
            Destroy(gameObject);
        }
        else if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.Destroyed();
            Destroy(gameObject);
        }
    }
}
