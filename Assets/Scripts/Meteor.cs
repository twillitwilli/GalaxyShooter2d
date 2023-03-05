using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private MeteorMovement _meteorParent;
    [SerializeField][Range(1, 100)]private float _lootChance;
    [SerializeField] [Range(1, 100)] private int _pointValue;
    private float _randomRotationSpeed;

    private void Start()
    {
        _meteorParent = GetComponentInParent<MeteorMovement>();
        float randomScale = Random.Range(0.25f, 0.7f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        _randomRotationSpeed = Random.Range(-90, 90);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * _randomRotationSpeed * Time.deltaTime);
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
        GameManager.instance.pointManager.UpdateCurrentScore(_pointValue);
        int randomSpawnChance = Random.Range(0, 100);
        if (randomSpawnChance < _lootChance) { GameManager.instance.powerUpManager.SpawnPowerUp(_meteorParent.transform); }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(_meteorParent.gameObject);
    }
}
