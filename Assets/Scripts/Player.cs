using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerStats playerStats;

    [SerializeField] private GameObject _playerExplosion, _playerThruster, _thrusterBoost, _tripleShotActive, _waveShotActive;

    private Vector3 _defaultThrusterPos = new Vector3(0, -2.59f, 0);
    private Vector3 _defaultThrusterSize = new Vector3(0.5f, 0.5f, 1);

    private Vector3 _boostedThrusterPos = new Vector3(0, -3.56f, 0);
    private Vector3 _boostedThrusterSize = new Vector3(1, 1, 1);

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.exploded = true;
            Destroy(meteor.gameObject);
            playerStats.AdjustCurrentHealth(-1);
        }
    }

    public void ActivateBoostThrusters(bool activate)
    {
        _thrusterBoost.SetActive(activate);
    }

    public void ThrusterSize(bool hasSpeedBoost)
    {
        if (!hasSpeedBoost)
        {
            _playerThruster.transform.localPosition = _defaultThrusterPos;
            _playerThruster.transform.localScale = _defaultThrusterSize;
        }
        else
        {
            _playerThruster.transform.localPosition = _boostedThrusterPos;
            _playerThruster.transform.localScale = _boostedThrusterSize;
        }
    }

    public void TripleShotActive(bool active)
    {
        _tripleShotActive.SetActive(active);
    }

    public void WaveShotActive(bool active)
    {
        if (active) { TripleShotActive(false); }
        _waveShotActive.SetActive(active);
    }

    public void PlayerDied()
    {
        Instantiate(_playerExplosion, transform.position, transform.rotation);
        GameManager.instance.cameraController.ShakeCamera();
        FindObjectOfType<EnemySpawner>().disableSpawner = true;
        Destroy(gameObject);
    }
}
