using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player player;
    private bool setFireCooldown;
    private float fireRateCooldown;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        PlayerMovement();
        if (CanFire() && Input.GetKeyDown(KeyCode.Space)) { FireLaser(); }
    }

    private void PlayerMovement()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(movementDirection * player.playerSpeed * Time.deltaTime);
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
        Instantiate(player.laserProjectile, laserSpawnOffset, transform.rotation);
        setFireCooldown = true;
    }

    private bool CanFire()
    {
        if (setFireCooldown)
        {
            fireRateCooldown = player.fireRate;
            setFireCooldown = false;
        }
        if (fireRateCooldown > 0) { fireRateCooldown -= Time.deltaTime; }
        else return true;
        return false;
    }
}
