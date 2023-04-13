using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUp : MonoBehaviour
{
    [HideInInspector] public Enemy _target;

    private void Update()
    {
        if (_target != null) { transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 1.5f * Time.deltaTime); }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() == _target)
        {
            _target.ActivateShield();
            Destroy(gameObject);
        }

        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.powerUpManager.PowerUpObtained(PowerUpManager.PowerUps.lockPlayer);
        }
    }
}
