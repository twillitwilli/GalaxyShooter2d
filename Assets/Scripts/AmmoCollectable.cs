using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GameManager.instance.player;
    }

    private void Update()
    {
        if (_player != null) { transform.position = Vector3.Lerp(transform.position, _player.transform.position, 0.005f); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentAmmo(1);
            Destroy(gameObject);
        }
    }
}
