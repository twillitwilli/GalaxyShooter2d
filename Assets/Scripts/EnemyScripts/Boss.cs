using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum BossPhase { idle, mirage, deciding, laserAttack, movingRandomPos, moveToCenter, plasmaExplosion, laserSpin, flee }
    private BossPhase _currentState;

    [HideInInspector] public EnemySpawner enemySpawner;

    [SerializeField] private GameObject _mirage, _bossLaser, _bomb, _laserSpin;

    private BoxCollider2D _bossCollider;
    private Animator _animator;
    private GivePoints _givePoints;

    private List<GameObject> _enemies = new List<GameObject>();

    private int _maxHealth, _currentHealth, _currentLaserAttack, _maxLaserAttack;
    private float _bossSpeed = 0.05f, _stoppingDistance = 1f;

    private bool _shootingLaser, _gotNewRandomPos, _bossCentered, _plasmaExplosion, _laserSpinning;
    private Vector3 _randomMovePosition;

    private void Start()
    {
        _bossCollider = GetComponent<BoxCollider2D>();
        _bossCollider.enabled = false;
        _animator = GetComponent<Animator>();
        _givePoints = GetComponent<GivePoints>();
        _enemies.Add(gameObject);
        _maxHealth = 50 + (10 * enemySpawner.GetCurrentLevel());
        _currentHealth = _maxHealth;
        GameManager.instance.displayManager.BossHealthDisplay(true);
        GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth);
        _currentState = BossPhase.idle;
        transform.position = new Vector3(0, 6, 0);
    }

    public void StartBossFight()
    {
        DecidingAttackPattern();
    }

    private void Update()
    {
        if (GameManager.instance.player == null) { _currentState = BossPhase.flee; }
        switch (_currentState)
        {
            case BossPhase.laserAttack:
                AimAtTarget(GameManager.instance.player.transform.position);
                if (!_shootingLaser)
                {
                    _shootingLaser = true;
                    StartCoroutine("LaserAttack");
                }
                break;

            case BossPhase.movingRandomPos:
                if (!_gotNewRandomPos)
                {
                    _gotNewRandomPos = true;
                    _randomMovePosition = GetRandomMovePosition();
                }
                else
                {
                    AimAtTarget(_randomMovePosition);
                    transform.position = Vector3.Lerp(transform.position, _randomMovePosition, _bossSpeed);
                    if (Vector3.Distance(transform.position, _randomMovePosition) <= _stoppingDistance) 
                    {
                        _shootingLaser = false;
                        _currentState = BossPhase.laserAttack; 
                    }
                }
                break;

            case BossPhase.plasmaExplosion:
                if (!_bossCentered) { MoveToCenterPosition(); }
                else
                {
                    if (!_plasmaExplosion)
                    {
                        _plasmaExplosion = true;
                        StartCoroutine("StartPlasmaExplosion");
                    }
                }
                break;

            case BossPhase.laserSpin:
                if (!_bossCentered) { MoveToCenterPosition(); }
                else
                {
                    _laserSpinning = true;
                    StartCoroutine("StartLaserSpin");
                }
                break;

            case BossPhase.mirage:
                break;

            case BossPhase.flee:
                DefaultRotation();
                transform.Translate(-Vector3.up * 5 * Time.deltaTime);
                if (transform.position.y <= -15) { BossKilled(false); }
                break;
        }
    }

    private void MoveToCenterPosition()
    {
        AimAtTarget(new Vector3(0, 0, 0));
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), _bossSpeed);
        if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) <= _stoppingDistance)
        {
            _bossCentered = true;
            transform.position = new Vector3(0, 0, 0);
            DefaultRotation();
        }
    }

    private Vector3 GetRandomMovePosition()
    {
        return new Vector3(Random.Range(-13.8f, 13.8f), Random.Range(-4.5f, 6.75f), 0);
    }

    private void DefaultRotation()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void AimAtTarget(Vector3 target)
    {
        //transform.up = transform.position - target;
    }

    public void AdjustBossHealth(int healthValue)
    {
        _currentHealth += healthValue;
        if (_currentHealth <= 0) { BossKilled(true); }
        else { GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth); }
    }

    public void DecidingAttackPattern()
    {
        _currentState = BossPhase.deciding;
        _bossCollider.enabled = true;
        DefaultRotation();
        int attackDecision = Random.Range(0, 5);
        switch (attackDecision)
        {
            case 1:
                _bossCentered = false;
                _currentState = BossPhase.plasmaExplosion;
                break;
            case 2:
                _bossCentered = false;
                _currentState = BossPhase.laserSpin;
                break;
            case 3:
                if ((_currentHealth / _maxHealth) < 0.5f) { _currentState = BossPhase.mirage; }
                else
                {
                    _shootingLaser = false;
                    _currentLaserAttack = 0;
                    _maxLaserAttack = Random.Range(3, 7);
                    _currentState = BossPhase.laserAttack;
                }
                break;
            default:
                _shootingLaser = false;
                _currentLaserAttack = 0;
                _maxLaserAttack = Random.Range(3, 7);
                _currentState = BossPhase.laserAttack;
                break;
        }
    }

    private IEnumerator LaserAttack()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 4));
        FireNormalLaser();
        _currentLaserAttack++;
        if (_currentLaserAttack < _maxLaserAttack) 
        {
            _gotNewRandomPos = false;
            _currentState = BossPhase.movingRandomPos; 
        }
        else { DecidingAttackPattern(); }
        
    }

    private void FireNormalLaser()
    {
        Instantiate(_bossLaser, transform.position, transform.rotation);
    }

    private IEnumerator StartPlasmaExplosion()
    {
        PlasmaExplosion();
        yield return new WaitForSeconds(12);
        DecidingAttackPattern();
    }

    private void PlasmaExplosion()
    {
        Instantiate(_bomb, transform.position, transform.rotation);
    }

    private IEnumerator StartLaserSpin()
    {
        LaserSpin();
        yield return new WaitForSeconds(12);
        DecidingAttackPattern();
    }

    private void LaserSpin()
    {
        Instantiate(_laserSpin, transform.position, transform.rotation);
    }

    private void ActivateMirageState()
    {
        _animator.Play("BossMirageSplit");
        _bossCollider.enabled = false;
        for (int i = 0; i < 2; i++) { SpawnMirages(); }
    }

    private void SpawnMirages()
    {
        GameObject mirage = Instantiate(_mirage, transform.position, transform.rotation);
        _enemies.Add(mirage);
    }

    private void BossKilled(bool playerKilled)
    {
        if (playerKilled)
        {
            _givePoints.GivePointsToPointManager();
        }
        GameManager.instance.displayManager.BossHealthDisplay(false);
        enemySpawner.IncreaseCurrentLevel();
        Destroy(gameObject);
    }
}
