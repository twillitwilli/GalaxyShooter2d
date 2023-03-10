using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpManager.PowerUps powerUp;

    private void Update()
    {
        transform.Translate(-Vector3.up * 2.5f * Time.deltaTime);
        if (transform.position.y < -6.85f) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GameManager.instance.powerUpManager.PowerUpObtained(powerUp);
            Destroy(gameObject);
        }
    }
}
