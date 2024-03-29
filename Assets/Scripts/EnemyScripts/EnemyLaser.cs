using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(-Vector3.up * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
        }

        PowerUp powerUp;
        if (collision.gameObject.TryGetComponent<PowerUp>(out powerUp))
        {
            Destroy(powerUp.gameObject);
        }
    }
}
