using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChance : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float _lootChance;

    public void Loot(Transform spawnLocation)
    {
        int randomSpawnChance = Random.Range(0, 100);
        if (randomSpawnChance < _lootChance) { GameManager.instance.powerUpManager.SpawnPowerUp(spawnLocation); }
    }
}
