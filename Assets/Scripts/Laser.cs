using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public Player player;
    public float laserSpeed;

    private void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
            enemyHit.AdjustHealth(-player.damage);
            Destroy(gameObject);
        }
    }
}
