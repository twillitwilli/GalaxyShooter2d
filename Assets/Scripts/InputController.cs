using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player _player;
    private bool _setFireCooldown;
    private float _fireRateCooldown;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!_player.playerDead)
        {
            PlayerMovement();
            if (CanFire() && Input.GetKeyDown(KeyCode.Space)) { FireLaser(); }
        }
    }

    private void PlayerMovement()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(movementDirection * _player.playerSpeed * Time.deltaTime);
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
        Vector3 laserSpawnOffset = new Vector3(transform.position.x, transform.position.y + 1.02f, 0);
        GameObject laserObject = Instantiate(_player.laserProjectile, laserSpawnOffset, transform.rotation);
        laserObject.GetComponent<Laser>().player = _player;
        _setFireCooldown = true;
    }

    private bool CanFire()
    {
        if (_setFireCooldown)
        {
            _fireRateCooldown = _player.fireRate;
            _setFireCooldown = false;
        }
        if (_fireRateCooldown > 0) { _fireRateCooldown -= Time.deltaTime; }
        else return true;
        return false;
    }
}
