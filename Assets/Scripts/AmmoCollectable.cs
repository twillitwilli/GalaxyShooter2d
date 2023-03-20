using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    private Player _player;
    [HideInInspector] public float speed = 0.0001f;

    private void Start()
    {
        CollectableManager.instance.spawnedAmmo.Add(this);
        transform.SetParent(CollectableManager.instance.transform);
        _player = GameManager.instance.player;
    }

    private void Update()
    {
        if (_player != null) { transform.position = Vector3.Lerp(transform.position, _player.transform.position, speed); }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentAmmo(Random.Range(1, 4));
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        CollectableManager.instance.spawnedAmmo.Remove(this);
    }
}
