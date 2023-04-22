using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _safeZone;
    private Player _player;

    private void Start()
    {
        SpawnSafeZones();
    }

    private void SpawnSafeZones()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f), 0);
            Instantiate(_safeZone, spawnPoint, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player == null && collision.gameObject.TryGetComponent<Player>(out _player))
        {
            _player.playerStats.AdjustCurrentHealth(-1);
            StartCoroutine("DamageOverTime");
        }

        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            Destroy(meteor.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_player != null && collision.gameObject.GetComponent<Player>())
        {
            _player = null;
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (_player != null)
        {
            yield return new WaitForSeconds(1.5f);
            if (!_player.IsInSafeZone()) { _player.playerStats.AdjustCurrentHealth(-1); }
        }
        
    }
}
