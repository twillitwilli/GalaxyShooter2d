using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMirage : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _defaultMat, _illusionMat;
    private int _currentHealth = 2, _maxHealth = 2;
    private GivePoints _givePoints;
    private LootChance _lootChance;

    private void Start()
    {
        _renderer.material = _defaultMat;
        _givePoints = GetComponent<GivePoints>();
        _lootChance = GetComponent<LootChance>();
    }

    public void AdjustCurrentHealth(int healthValue)
    {
        _currentHealth -= healthValue;
        if ((_currentHealth / _maxHealth) < 0.5f) { _renderer.material = _illusionMat; }
        if (_currentHealth <= 0) { MirageFaded(true); }
    }

    public void AdjustMaxHealth(int newMaxHealthValue)
    {
        _maxHealth = newMaxHealthValue;
        _currentHealth = _maxHealth;
    }

    public void MirageFaded(bool killedByPlayer)
    {
        if (killedByPlayer)
        {
            _givePoints.GivePointsToPointManager();
            _lootChance.Loot(transform);
        }
        Destroy(gameObject);
    }
}
