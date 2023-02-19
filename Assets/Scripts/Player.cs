using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed, fireRate;
    [HideInInspector] public bool canFire, setFireCooldown;
    public GameObject laserProjectile;
    private float fireRateCooldown;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
            enemyHit.EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
