using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager instance;
    [HideInInspector] public List<PowerUp> spawnedPowerUps = new List<PowerUp>();
    [HideInInspector] public List<AmmoCollectable> spawnedAmmo = new List<AmmoCollectable>();

    private void Start()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }

    public void CollectAmmo()
    {
        for (int i = 0; i < spawnedAmmo.Count; i++)
        {
            spawnedAmmo[i].speed *= 2;
        }
    }    
}
