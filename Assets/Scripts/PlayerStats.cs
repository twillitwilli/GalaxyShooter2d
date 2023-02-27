using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player _player;
    [HideInInspector] public PowerUpManager powerUpManager;
    private SpriteRenderer _playerRenderer;
    private float _maxHealth = 100, _playerSpeed = 15, _fireRate = 0.25f, _attackDamage = 12, _currentHealth;
    private WaitForSeconds _colorChangeWaitTime = new WaitForSeconds(0.2f);
    private Color _defaualtColor = new Color(255, 255, 255, 255);
    private Color _playerHitColor = new Color(255, 0, 0, 255);

    private void Start()
    {
        _player = GetComponent<Player>();
        powerUpManager = GameManager.instance.powerUpManager;
        _playerRenderer = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
    }

    public void AdjustCurrentHealth(float healthValue)
    {
        if (healthValue < 0) { StartCoroutine("PlayerHitColorChange"); }
        _currentHealth += healthValue;
        if (_currentHealth > _maxHealth) { _currentHealth = _maxHealth; }
        else if (_currentHealth < 0) { _player.PlayerDied(); }
    }

    private IEnumerator PlayerHitColorChange()
    {
        _playerRenderer.color = _playerHitColor;
        yield return _colorChangeWaitTime;
        _playerRenderer.color = _defaualtColor;
    }

    public void AdjustMaxHealth(float healthValue)
    {
        _maxHealth += healthValue;
        if (_maxHealth > 350) { _maxHealth = 350; }
        else if (_maxHealth < 100) { _maxHealth = 100; }
    }

    public void AdjustPlayerSpeed(float speedValue)
    {
        _playerSpeed += speedValue;
        if (_playerSpeed > 30) { _playerSpeed = 30; }
        else if (_playerSpeed < 15) { _playerSpeed = 15; }
    }

    public void AdjustFireRate(float fireRateValue)
    {
        _fireRate += fireRateValue;
        if (_fireRate < 0.1f) { _fireRate = 0.1f; }
        else if (_fireRate > 0.25f) { _fireRate = 0.25f; }
    }

    public void AdjustAttackDamage(float attackDamageValue)
    {
        _attackDamage += attackDamageValue;
        if (_attackDamage > 100) { _attackDamage = 100; }
        else if (_attackDamage < 12) { _attackDamage = 12; }
    }

    public float GetPlayerSpeed()
    {
        return _playerSpeed;
    }

    public float GetFireRate()
    {
        return _fireRate;
    }

    public float GetAttackDamage()
    {
        return _attackDamage;
    }
}
