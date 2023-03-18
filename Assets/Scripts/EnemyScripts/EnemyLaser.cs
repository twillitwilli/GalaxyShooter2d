using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(-Vector3.up * 10 * Time.deltaTime);
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
