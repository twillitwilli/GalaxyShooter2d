using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;

    [HideInInspector] public PlayerStats playerStats;

    [SerializeField] private GameObject _playerExplosion, _playerThruster, _thrusterBoost, _tripleShotActive, _waveShotActive, _playerLocked;

    private Vector3 _defaultThrusterPos = new Vector3(0, -2.59f, 0);
    private Vector3 _defaultThrusterSize = new Vector3(0.5f, 0.5f, 1);

    private Vector3 _boostedThrusterPos = new Vector3(0, -3.56f, 0);
    private Vector3 _boostedThrusterSize = new Vector3(1, 1, 1);

    private bool _inSafeZone;

    private void Start()
    {
        _gameManager = GameManager.instance;
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

    public void PlayerLockedEffect(bool active)
    {
        if (active && !_playerLocked.activeSelf) { _playerLocked.SetActive(true); }
        else { _playerLocked.SetActive(false); }
    }

    public void EnterSafeZone(bool entered)
    {
        if (entered) { _inSafeZone = true; }
        else { _inSafeZone = false; }
    }

    public bool IsInSafeZone()
    {
        return _inSafeZone;
    }

    public void PlayerDied()
    {
        if (!GameManager.instance.IsGodModeActive())
        {
            Instantiate(_playerExplosion, transform.position, transform.rotation);
            _gameManager.cameraController.ShakeCamera();
            _gameManager.displayManager.SaveHighScore();
            _gameManager.displayManager.ToggleNotification(true, 0);
            FindObjectOfType<EnemySpawner>().disableSpawner = true;
            Destroy(gameObject);
        }
    }
}
