using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float randomSpeed, randomRotationSpeed;
    [SerializeField][Range(1, 100)]private float lootChance;

    private void Start()
    {
        float randomScale = Random.Range(0.5f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        randomSpeed = Random.Range(1, 6);
    }

    private void Update()
    {
        transform.Translate(-Vector3.up * randomSpeed * Time.deltaTime);
        if (transform.position.y < -6.9f) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-10);
            Destroy(gameObject);
        }
    }

    public void Destroyed()
    {
        int randomSpawnChance = Random.Range(0, 100);
        if (randomSpawnChance < lootChance) { GameManager.instance.powerUpManager.SpawnPowerUp(transform); }
        Destroy(gameObject);
    }
}
