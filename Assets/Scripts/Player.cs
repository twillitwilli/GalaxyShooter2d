using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerStats playerStats;
    [SerializeField] private GameObject _playerExplosion, _playerThruster;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ThrusterSize(bool hasSpeedBoost)
    {
        if (!hasSpeedBoost)
        {
            _playerThruster.transform.localPosition = new Vector3(0, -2.59f, 0);
            _playerThruster.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            _playerThruster.transform.localPosition = new Vector3(0, -3.56f, 0);
            _playerThruster.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SpeedBoostThruster()
    {
        
    }

    public void PlayerDied()
    {
        Instantiate(_playerExplosion, transform.position, transform.rotation);
        FindObjectOfType<EnemySpawner>().disableSpawner = true;
        FindObjectOfType<EnvironmentSpawner>().disableSpawner = true;
        Destroy(gameObject);
    }
}
