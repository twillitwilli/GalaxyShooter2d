using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerStats playerStats;
    [SerializeField] private GameObject _playerExplosion;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void PlayerDied()
    {
        Instantiate(_playerExplosion, transform.position, transform.rotation);
        FindObjectOfType<EnemySpawner>().disableSpawner = true;
        FindObjectOfType<EnvironmentSpawner>().disableSpawner = true;
        Destroy(gameObject);
    }
}
