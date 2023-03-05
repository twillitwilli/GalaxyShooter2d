using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float _laserSpeed = 10;

    private void Update()
    {
        transform.Translate(-Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y < -10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
        }
    }
}
