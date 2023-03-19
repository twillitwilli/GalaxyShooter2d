using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererLaser : MonoBehaviour
{
    [HideInInspector] public GathererEnemy gatherer;

    private void Update()
    {
        transform.Translate(-Vector3.up * 10 * Time.deltaTime);
        if (transform.position.y < -10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.exploded = true;
            Destroy(meteor.gameObject);
            LootChance();
            Destroy(gameObject);
        }
    }

    private void LootChance()
    {
        float lootChance = Random.Range(0, 100);
        if (lootChance > 85) { gatherer.ObtainedPowerUp(); }
    }
}
