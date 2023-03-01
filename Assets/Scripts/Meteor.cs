using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private MeteorMovement _meteorParent;
    [SerializeField][Range(1, 100)]private float lootChance;
    [SerializeField] [Range(1, 100)] private int pointValue;
    private float randomRotationSpeed;

    private void Start()
    {
        _meteorParent = GetComponentInParent<MeteorMovement>();
        float randomScale = Random.Range(0.5f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        randomRotationSpeed = Random.Range(-90, 90);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * randomRotationSpeed * Time.deltaTime);
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
        GameManager.instance.pointManager.UpdateCurrentScore(pointValue);
        int randomSpawnChance = Random.Range(0, 100);
        if (randomSpawnChance < lootChance) { GameManager.instance.powerUpManager.SpawnPowerUp(_meteorParent.transform); }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(_meteorParent.gameObject);
    }
}
