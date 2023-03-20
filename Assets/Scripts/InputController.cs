using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player _player;
    private PlayerStats _playerStats;
    [SerializeField] private GameObject _laser, _tripleShot, _meteorMiner, _waveAttack;
    private bool _setFireCooldown;
    private float _fireRateCooldown;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!_playerStats.powerUpManager.IsPlayerLocked())
        {
            _player.PlayerLockedEffect(false);

            if (_playerStats.ThrusterFuel() > 20 && !_playerStats.BoostActive() && Input.GetKeyDown(KeyCode.LeftShift)) { _playerStats.ThrusterBoost(true, 8); }
            else if (_playerStats.BoostActive() && Input.GetKeyUp(KeyCode.LeftShift)) { _playerStats.ThrusterBoost(false, -8); }

            PlayerMovement();

            if (CanFire() && Input.GetKeyDown(KeyCode.Space)) { FireLaser(); }

            if (!_meteorMiner.activeSelf && Input.GetKeyDown(KeyCode.RightShift)) { _meteorMiner.SetActive(true); }
            else if (_meteorMiner.activeSelf && Input.GetKeyUp(KeyCode.RightShift)) { _meteorMiner.SetActive(false); }

            if (Input.GetKeyDown(KeyCode.C)) { CollectableManager.instance.CollectAmmo(); }
        }
        else { _player.PlayerLockedEffect(true); }
    }

    private void PlayerMovement()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (!_playerStats.powerUpManager.IsSpeedBoostActive())
        {
            transform.Translate(movementDirection * _playerStats.GetPlayerSpeed() * Time.deltaTime);
        }
        else { transform.Translate(movementDirection * (_playerStats.GetPlayerSpeed() + 5) * Time.deltaTime); }
        CheckPlayerBounds();
    }

    private void CheckPlayerBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.5f, 5), 0);
        if (transform.position.x >= 11.5f) { transform.position = new Vector3(-11.5f, transform.position.y, 0); }
        else if (transform.position.x <= -11.5f) { transform.position = new Vector3(11.5f, transform.position.y, 0); }
    }

    private void FireLaser()
    {
        if (!_playerStats.powerUpManager.IsWaveAttackActive() && _playerStats.UseAmmo(1))
        {
            if (!_playerStats.powerUpManager.IsTripleShotActive())
            {
                Vector3 laserSpawnOffset = new Vector3(transform.position.x, transform.position.y + 0.518f, 0);
                GameObject laserObject = Instantiate(_laser, laserSpawnOffset, transform.rotation);
                laserObject.GetComponent<Laser>().player = _player;
            }
            else
            {
                GameObject tripleShotParent = Instantiate(_tripleShot, transform.position, transform.rotation);
                Laser[] laserShots = tripleShotParent.GetComponentsInChildren<Laser>();
                foreach (Laser lasers in laserShots) { lasers.player = _player; }
            }
        }
        else if (_playerStats.powerUpManager.IsWaveAttackActive())
        {
            Vector3 waveSpawnOffset = new Vector3(transform.position.x, transform.position.y + 0.87f, 0);
            GameObject newWaveAttack = Instantiate(_waveAttack, waveSpawnOffset, transform.rotation);
            newWaveAttack.transform.localEulerAngles = new Vector3(0, 0, 90);
            newWaveAttack.GetComponent<WaveAttack>().player = _player;
        }

        _setFireCooldown = true;
    }

    private bool CanFire()
    {
        if (_setFireCooldown)
        {
            _fireRateCooldown = _playerStats.GetFireRate();
            _setFireCooldown = false;
        }
        if (_fireRateCooldown > 0) { _fireRateCooldown -= Time.deltaTime; }
        else return true;
        return false;
    }
}
