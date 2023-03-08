using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private Player _player;
    public enum PowerUps { tripleShot, speedBoost, shield }
    [SerializeField] private GameObject[] _powerUp;

    private bool _tripleShotActive, _speedBoostActive;
    private WaitForSeconds _tripleShotDuration = new WaitForSeconds(8);
    private WaitForSeconds _speedBoostDuration = new WaitForSeconds(10);

    [SerializeField] private GameObject _shieldObject;
    private int _shieldHealth;
    private GameObject _activeShield;
    [SerializeField]private Color[] _shieldStrength;


    private void Start()
    {
        SetNewPlayer();
    }

    public void SetNewPlayer()
    {
        _player = GameManager.instance.player;
    }

    public void SpawnPowerUp(Transform spawnTransform)
    {
        int randomPowerUp = Random.Range(0, _powerUp.Length);
        Instantiate(_powerUp[randomPowerUp], spawnTransform.position, spawnTransform.rotation);
    }

    public void PowerUpObtained(PowerUps whichEffectObtained)
    {
        switch (whichEffectObtained)
        {
            case PowerUps.tripleShot:
                if (!_tripleShotActive)
                {
                    PlayPowerUpObtainedSFX();
                    _tripleShotActive = true;
                    StartCoroutine("TripleShotDuration");
                }
                break;

            case PowerUps.speedBoost:
                if (!_speedBoostActive)
                {
                    PlayPowerUpObtainedSFX();
                    _speedBoostActive = true;
                    _player.ThrusterSize(true);
                    StartCoroutine("SpeedBoostDuration");
                }
                break;

            case PowerUps.shield:
                if (_activeShield == null)
                {
                    _shieldHealth = 3;
                    PlayPowerUpObtainedSFX();
                    _activeShield = Instantiate(_shieldObject, _player.transform.position, _player.transform.rotation);
                    _activeShield.transform.SetParent(_player.transform);
                }
                break;
        }
    }

    private void PlayPowerUpObtainedSFX()
    {
        _player.GetComponent<AudioSource>().Play();
    }

    public bool IsTripleShotActive()
    {
        return _tripleShotActive;
    }

    public bool IsSpeedBoostActive()
    {
        return _speedBoostActive;
    }

    public bool ShieldActive()
    {
        if (_activeShield != null)
        {
            _shieldHealth--;
            if (_shieldHealth <= 0) { Destroy(_activeShield); }
            else { _activeShield.GetComponent<SpriteRenderer>().color = _shieldStrength[_shieldHealth - 1]; }
            return true;
        }
        else return false;
    }

    private IEnumerator TripleShotDuration()
    {
        yield return _tripleShotDuration;
        _tripleShotActive = false;
    }

    private IEnumerator SpeedBoostDuration()
    {
        yield return _speedBoostDuration;
        _speedBoostActive = false;
        if (_player != null) { _player.ThrusterSize(false); }
    }
}
