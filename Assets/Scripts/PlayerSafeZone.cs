using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafeZone : MonoBehaviour
{
    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _player))
        {
            _player.EnterSafeZone(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_player != null && collision.gameObject.GetComponent<Player>())
        {
            _player.EnterSafeZone(false);
        }
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.EnterSafeZone(false);
        }
    }
}
