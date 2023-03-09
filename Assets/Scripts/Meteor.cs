using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [HideInInspector] public EnvironmentSpawner spawner;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _meteorGatheredSFX;
    private MeteorMovement _meteorParent;
    private float _randomRotationSpeed;
    private GivePoints _givePoints;
    private LootChance _lootChance;

    private void Awake()
    {
        _meteorParent = GetComponentInParent<MeteorMovement>();
        _givePoints = GetComponent<GivePoints>();
        _lootChance = GetComponent<LootChance>();
    }

    private void Start()
    {
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
            player.playerStats.AdjustCurrentHealth(-1);
            Destroy(gameObject);
        }
    }

    public void Destroyed()
    {
        spawner.meteorsDestroyed++;
        GameObject newExplosion = Instantiate(_explosion, transform.position, transform.rotation);
        newExplosion.transform.localScale = transform.localScale;
        GameManager.instance.cameraController.ShakeCamera();
        _givePoints.GivePointsToPointManager();
        _lootChance.Loot(_meteorParent.transform);
        Destroy(gameObject);
    }

    public void Gathered()
    {
        AudioSource.PlayClipAtPoint(_meteorGatheredSFX, transform.position);
        spawner.meteorsGathered++;
        if (!spawner.enemySpawnerActive && spawner.meteorsGathered >= 10) { spawner.TurnOnEnemySpawns(); }
        _lootChance.Loot(_meteorParent.transform);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(_meteorParent.gameObject);
    }
}
