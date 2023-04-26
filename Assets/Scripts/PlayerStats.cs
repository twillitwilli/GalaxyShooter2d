using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player _player;
    [HideInInspector] public PowerUpManager powerUpManager;
    private SpriteRenderer _playerRenderer;
    private int _maxHealth = 3, _currentHealth, _currentAmmo, _attackDamage = 1, _homingMissiles = 2;
    private float _playerSpeed = 10, _fireRate = 0.25f, _thrustFuel = 100;
    private WaitForSeconds _colorChangeWaitTime = new WaitForSeconds(0.2f);
    private Color _defaualtColor = new Color(255, 255, 255, 255);
    private Color _playerHitColor = new Color(255, 0, 0, 255);
    [SerializeField] private GameObject[] _playerDamageEffect;
    [SerializeField] private GameObject[] _homingMissleVisuals;
    private bool _boosterControl;

    private void Start()
    {
        _player = GetComponent<Player>();
        powerUpManager = GameManager.instance.powerUpManager;
        _playerRenderer = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
        _currentAmmo = 15;
        GameManager.instance.displayManager.UpdateAmmoDisplay(_currentAmmo);
        AdjustCurrentHealth(0);
        AdjustCurrentHomingMissiles(0);
    }

    public void Update()
    {
        if (!BoostActive() && _thrustFuel < 100) { RechargeThrusters(); }
        else if (BoostActive()) { UsingFuel(); }
    }

    public void AdjustCurrentHealth(int healthValue)
    {
        if (!_player.IsInSafeZone())
        {
            if (powerUpManager.ShieldActive() && healthValue < 0) { return; }

            if (healthValue < 0) { StartCoroutine("PlayerHitColorChange"); }

            _currentHealth += healthValue;
            VisualDamage();

            if (_currentHealth > _maxHealth) { _currentHealth = _maxHealth; }
            else if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                GameManager.instance.displayManager.UpdateHealthDisplay(_currentHealth);
                _player.PlayerDied();
                return;
            }

            GameManager.instance.displayManager.UpdateHealthDisplay(_currentHealth);
        }
    }

    private IEnumerator PlayerHitColorChange()
    {
        _playerRenderer.color = _playerHitColor;
        yield return _colorChangeWaitTime;
        _playerRenderer.color = _defaualtColor;
    }

    private void VisualDamage()
    {
        switch (_currentHealth)
        {
            case 3:
                foreach (GameObject visualDamage in _playerDamageEffect)
                {
                    visualDamage.SetActive(false);
                }
                break;
            case 2:
                _playerDamageEffect[0].SetActive(true);
                _playerDamageEffect[1].SetActive(false);
                break;
            case 1:
                foreach (GameObject visualDamage in _playerDamageEffect)
                {
                    visualDamage.SetActive(true);
                }
                break;
        }
    }

    public void AdjustMaxHealth(int healthValue)
    {
        _maxHealth += healthValue;
        if (_maxHealth > 350) { _maxHealth = 350; }
        else if (_maxHealth < 100) { _maxHealth = 100; }
    }

    public void AdjustPlayerSpeed(float speedValue)
    {
        _playerSpeed += speedValue;
        if (_playerSpeed > 30) { _playerSpeed = 30; }
        else if (_playerSpeed < 10) { _playerSpeed = 10; }
    }

    public void AdjustFireRate(float fireRateValue)
    {
        _fireRate += fireRateValue;
        if (_fireRate < 0.1f) { _fireRate = 0.1f; }
        else if (_fireRate > 0.25f) { _fireRate = 0.25f; }
    }

    public void AdjustAttackDamage(int attackDamageValue)
    {
        _attackDamage += attackDamageValue;
        if (_attackDamage > 5) { _attackDamage = 5; }
        else if (_attackDamage < 1) { _attackDamage = 1; }
    }

    public void AdjustCurrentAmmo(int ammoValue)
    {
        _currentAmmo += ammoValue;
        if (_currentAmmo < 0) { _currentAmmo = 0; }
        else if (_currentAmmo > 15) { _currentAmmo = 15; }
        GameManager.instance.displayManager.UpdateAmmoDisplay(_currentAmmo);
    }

    public int GetPlayerHealth()
    {
        return _currentHealth;
    }

    public float GetPlayerSpeed()
    {
        return _playerSpeed;
    }

    public float GetFireRate()
    {
        return _fireRate;
    }

    public int GetAttackDamage()
    {
        return _attackDamage;
    }

    public bool UseAmmo(int ammoUsed)
    {
        if (_currentAmmo >= ammoUsed)
        {
            AdjustCurrentAmmo(-ammoUsed);
            return true;
        }
        else return false;
    }

    public void ThrusterBoost(bool boost, float boostSpeed)
    {
        _boosterControl = boost;
        _player.ActivateBoostThrusters(boost);
        AdjustPlayerSpeed(boostSpeed);
    }

    private void RechargeThrusters()
    {
        AdjustThrustFuel(Time.deltaTime * 1.5f);
        GameManager.instance.displayManager.UpdateFuelDisplay(_thrustFuel);
    }

    private void UsingFuel()
    {
        AdjustThrustFuel(Time.deltaTime * -8);
        GameManager.instance.displayManager.UpdateFuelDisplay(_thrustFuel);
    }

    public void AdjustThrustFuel(float fuelValue)
    {
        _thrustFuel += fuelValue;
        if (_thrustFuel > 100) { _thrustFuel = 100; }
        else if (_thrustFuel < 0) 
        { 
            _thrustFuel = 0;
            ThrusterBoost(false, -8);
        }
    }

    public bool BoostActive()
    {
        return _boosterControl;
    }

    public float ThrusterFuel()
    {
        return _thrustFuel;
    }

    public void AdjustCurrentHomingMissiles(int missileValue)
    {
        _homingMissiles += missileValue;
        if (_homingMissiles >= 4)
        {
            _homingMissiles = 4;
            foreach (GameObject missileObj in _homingMissleVisuals) { missileObj.SetActive(true); }
        }
        else if (_homingMissiles <= 0)
        {
            _homingMissiles = 0;
            foreach (GameObject missileObj in _homingMissleVisuals) { missileObj.SetActive(false); }
        }
        else
        {
            switch (_homingMissiles)
            {
                case 3:
                    foreach (GameObject missileObj in _homingMissleVisuals) { missileObj.SetActive(true); }
                    _homingMissleVisuals[3].SetActive(false);
                    break;
                case 2:
                    foreach (GameObject missileObj in _homingMissleVisuals) { missileObj.SetActive(true); }
                    _homingMissleVisuals[3].SetActive(false);
                    _homingMissleVisuals[2].SetActive(false);
                    break;
                case 1:
                    foreach (GameObject missileObj in _homingMissleVisuals) { missileObj.SetActive(false); }
                    _homingMissleVisuals[0].SetActive(true);
                    break;
            }
        }
    }

    public int CurrentMissiles()
    {
        return _homingMissiles;
    }

    public Transform FireMissile()
    {
        return _homingMissleVisuals[_homingMissiles - 1].transform;
    }
}
