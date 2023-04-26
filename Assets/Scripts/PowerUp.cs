using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpManager.PowerUps powerUp;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        CollectableManager.instance.spawnedPowerUps.Add(this);
        transform.SetParent(CollectableManager.instance.transform);
    }

    private void Update()
    {
        transform.Translate(-Vector3.up * 2.5f * Time.deltaTime);
        if (transform.position.y < -10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GameManager.instance.powerUpManager.PowerUpObtained(powerUp);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        CollectableManager.instance.spawnedPowerUps.Remove(this);
    }
}
