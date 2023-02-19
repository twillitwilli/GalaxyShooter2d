using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed;

    private void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Object - " + other.gameObject);
        if (other.gameObject.GetComponent<Enemy>())
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            Instantiate(enemyHit.enemyExplosion, enemyHit.transform.position, enemyHit.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
