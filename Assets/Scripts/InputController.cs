using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player _player;
    [SerializeField] private GameObject _laser, _tripleShot;
    private bool _setFireCooldown;
    private float _fireRateCooldown;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        PlayerMovement();
        if (CanFire() && Input.GetKeyDown(KeyCode.Space)) { FireLaser(); }
    }

    private void PlayerMovement()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (!_player.playerStats.powerUpManager.IsSpeedBoostActive())
        {
            transform.Translate(movementDirection * _player.playerStats.GetPlayerSpeed() * Time.deltaTime);
        }
        else { transform.Translate(movementDirection * (_player.playerStats.GetPlayerSpeed() + 5) * Time.deltaTime); }
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
        if (!_player.playerStats.powerUpManager.IsTripleShotActive())
        {
            Vector3 laserSpawnOffset = new Vector3(transform.position.x, transform.position.y + 1.037f, 0);
            GameObject laserObject = Instantiate(_laser, laserSpawnOffset, transform.rotation);
            laserObject.GetComponent<Laser>().player = _player;
        }
        else
        {
            GameObject tripleShotParent = Instantiate(_tripleShot, transform.position, transform.rotation);
            Laser[] laserShots = tripleShotParent.GetComponentsInChildren<Laser>();
            foreach (Laser lasers in laserShots) { lasers.player = _player; }
        }
        _setFireCooldown = true;
    }

    private bool CanFire()
    {
        if (_setFireCooldown)
        {
            _fireRateCooldown = _player.playerStats.GetFireRate();
            _setFireCooldown = false;
        }
        if (_fireRateCooldown > 0) { _fireRateCooldown -= Time.deltaTime; }
        else return true;
        return false;
    }
}
