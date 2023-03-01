using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private Player _player;
    public enum PowerUps { tripleShot, speedBoost, shield }
    [SerializeField] private GameObject[] powerUp;
    [SerializeField] private GameObject shieldObject;
    private bool _tripleShotActive, _speedBoostActive;
    private WaitForSeconds tripleShotDuration = new WaitForSeconds(8);
    private WaitForSeconds speedBoostDuration = new WaitForSeconds(10);
    private GameObject activeShield;

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
        int randomPowerUp = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[randomPowerUp], spawnTransform.position, spawnTransform.rotation);
    }

    public void PowerUpObtained(PowerUps whichEffectObtained)
    {
        switch (whichEffectObtained)
        {
            case PowerUps.tripleShot:
                _tripleShotActive = true;
                StartCoroutine("TripleShotDuration");
                break;
            case PowerUps.speedBoost:
                _speedBoostActive = true;
                StartCoroutine("SpeedBoostDuration");
                break;
            case PowerUps.shield:
                if (activeShield == null)
                {
                    activeShield = Instantiate(shieldObject, _player.transform.position, _player.transform.rotation);
                    activeShield.transform.SetParent(_player.transform);
                }
                break;
        }
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
        if (activeShield != null)
        {
            Destroy(activeShield);
            return true;
        }
        else return false;
    }

    private IEnumerator TripleShotDuration()
    {
        yield return tripleShotDuration;
        _tripleShotActive = false;
    }

    private IEnumerator SpeedBoostDuration()
    {
        yield return speedBoostDuration;
        _speedBoostActive = false;
    }
}
