using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum BossPhase { comeIntoTheScreen, flee, laserAttack, plasmaExplosion, laserSpin, mirage }
    private BossPhase _currentState;

    [HideInInspector] public EnemySpawner enemySpawner;

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _normalMat, _mirageMat;

    [SerializeField] private GameObject _mirage, _bossLaser, _bomb, _laserSpin, _bossExplosion;

    private BoxCollider2D _bossCollider;
    private Animator _animator;
    private GivePoints _givePoints;
    private Player _player;

    private List<BossMirage> _mirages = new List<BossMirage>();

    private int _maxHealth, _currentHealth, _laserShotsFired, _maxMirages, _mirageHealth;
    private float _bossSpeed = 6, _laserCooldownTimer, _attackCooldownTimer;

    private bool _thinking, _movingLeft, _setLaserCooldown, _plasmaExplosion, _laserSpinning, _mirageActive, _bossDead, _setAttackCooldown;
    private Vector3 _randomMovePosition;

    private void Start()
    {
        _bossCollider = GetComponent<BoxCollider2D>();
        _bossCollider.enabled = false;
        _animator = GetComponent<Animator>();
        _givePoints = GetComponent<GivePoints>();
        _player = GameManager.instance.player;

        _maxHealth = 100 + (10 * enemySpawner.GetCurrentLevel());
        _currentHealth = _maxHealth;
        GameManager.instance.displayManager.BossHealthDisplay(true);
        GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth);
        _currentState = BossPhase.comeIntoTheScreen;
        transform.position = new Vector3(0, 15, 0);
        RandomMovePosition(true);
        _maxMirages = 3 + Mathf.RoundToInt(enemySpawner.GetCurrentLevel() * 0.25f);
        if (_maxMirages > 6) { _maxMirages = 6; }
    }

    private void Update()
    {
        if(!_bossDead)
        {
            if (GameManager.instance.player == null) { _currentState = BossPhase.flee; }

            if (!_thinking)
            {
                switch (_currentState)
                {
                    case BossPhase.comeIntoTheScreen:
                        Movement();
                        if (transform.position.y <= 0)
                        {
                            _bossCollider.enabled = true;
                            StartCoroutine("Thinking");
                        }
                        break;

                    case BossPhase.laserAttack:
                        AimAtTarget(_player.transform.position);
                        Movement();
                        LaserAttack();
                        break;

                    case BossPhase.plasmaExplosion:
                        if (transform.position != new Vector3(0, 0, 0)) { Movement(); }
                        else { StartCoroutine("PlasmaExplosion"); }
                        break;

                    case BossPhase.laserSpin:
                        if (transform.position != new Vector3(0, 0, 0)) { Movement(); }
                        else { StartCoroutine("LaserSpin"); }
                        break;

                    case BossPhase.mirage:
                        if (!_mirageActive && transform.position != new Vector3(0,0,0)) { Movement(); }
                        else if (!_mirageActive && transform.position == new Vector3(0,0,0)) { _animator.Play("BossMirageSplit"); }
                        else
                        {
                            AimAtTarget(_player.transform.position);
                            Movement();
                            LaserAttack();
                        }
                        break;

                    case BossPhase.flee:
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                        transform.Translate(-Vector3.up * 5 * Time.deltaTime);
                        if (transform.position.y <= -15)
                        {
                            BossKilled(false);
                            Destroy(gameObject);
                        }
                        break;
                }
            }

            if (!AttackCooldown()) { } //attack cooldown function
        }
    }

    private void Movement()
    {
        switch(_currentState)
        {
            case BossPhase.comeIntoTheScreen:
                transform.Translate(-Vector3.up * _bossSpeed * Time.deltaTime);
                break;

            case BossPhase.laserAttack:
                SideToSideMovement();
                break;

            case BossPhase.mirage:
                if (!_mirageActive) { MoveToCenter(); }
                else { SideToSideMovement(); }
                break;

            default:
                MoveToCenter();
                break;
        }
    }

    private void MoveToCenter()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), _bossSpeed * Time.deltaTime);
        if (transform.position == new Vector3(0, 0, 0))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void SideToSideMovement()
    {
        if (_movingLeft && _randomMovePosition.x >= 0) { RandomMovePosition(true); }
        else if (!_movingLeft && _randomMovePosition.x <= 0) { RandomMovePosition(false); }

        transform.position = Vector3.MoveTowards(transform.position, _randomMovePosition, _bossSpeed * Time.deltaTime * 0.75f);
        if (transform.position == _randomMovePosition)
        {
            if (_movingLeft) { _movingLeft = false; }
            else { _movingLeft = true; }
        }
    }

    private IEnumerator Thinking()
    {
        _thinking = true;
        yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
        int attackDecision = Random.Range(0, 8);
        switch (attackDecision)
        {
            case 1:
                _plasmaExplosion = false;
                _currentState = BossPhase.plasmaExplosion;
                break;

            case 2:
                _laserSpinning = false;
                _currentState = BossPhase.laserSpin;
                break;

            case 3:
                float health = _currentHealth;
                float maxHealth = _maxHealth;

                if ((health / maxHealth) < 0.5f)
                {
                    _mirageHealth = 3 + enemySpawner.GetCurrentLevel();
                    _currentState = BossPhase.mirage;
                }
                else
                {
                    _setLaserCooldown = true;
                    _laserShotsFired = 0;
                    _currentState = BossPhase.laserAttack;
                }
                break;

            default:
                _setLaserCooldown = true;
                _laserShotsFired = 0;
                _currentState = BossPhase.laserAttack;
                break;
        }

        _thinking = false;
        _setAttackCooldown = true;
    }

    private bool AttackCooldown()
    {
        if (_setAttackCooldown)
        {
            _attackCooldownTimer = 5;
            _setAttackCooldown = false;
        }

        if (_attackCooldownTimer > 0) { _attackCooldownTimer -= Time.deltaTime; }
        else return true;

        return false;
    }

    private void AimAtTarget(Vector3 target)
    {
        transform.up = transform.position - target;
    }

    private void RandomMovePosition(bool isMovingLeft)
    {
        float randomXPos = Random.Range(0.1f, 12);
        if (isMovingLeft) { randomXPos *= -1; }
        _randomMovePosition = new Vector3(randomXPos, Random.Range(0, 6.5f), 0);
    }

    private void LaserAttack()
    {
        if (_setLaserCooldown)
        {
            _laserCooldownTimer = 3;
            _setLaserCooldown = false;
        }
        
        if (_laserCooldownTimer > 0) { _laserCooldownTimer -= Time.deltaTime; }
        else
        {
            _laserShotsFired++;
            Instantiate(_bossLaser, transform.position, transform.rotation);
            if (_laserShotsFired >= 5 && !_mirageActive) 
            {
                StartCoroutine("Thinking"); 
            }
            _setLaserCooldown = true;
        }
    }

    public void AdjustBossHealth(int healthValue)
    {
        if (!_mirageActive)
        {
            _currentHealth += healthValue;
            if (_currentHealth <= 0) { BossKilled(true); }
            else { GameManager.instance.displayManager.UpdateBossHealth(_maxHealth, _currentHealth); }
        }
        else
        {
            _mirageHealth += healthValue;
            if (_mirageHealth <= 0) 
            { 
                if (_mirages.Count > 0)
                {
                    foreach (BossMirage spawnedMirages in _mirages)
                    {
                        spawnedMirages.MirageFaded(false);
                    }
                }
                StartCoroutine("LeaveMirageState"); 
            }
        }
    }

    private IEnumerator PlasmaExplosion()
    {
        if (AttackCooldown())
        {
            _setAttackCooldown = true;
            if (!_plasmaExplosion)
            {
                Instantiate(_bomb, transform.position, transform.rotation);
                _plasmaExplosion = true;
            }
            yield return new WaitForSeconds(10);
            StartCoroutine("Thinking");
        }
    }

    private IEnumerator LaserSpin()
    {
        if (AttackCooldown())
        {
            _setAttackCooldown = true;
            if (!_laserSpinning)
            {
                Instantiate(_laserSpin, transform.position, transform.rotation);
                _laserSpinning = true;
            }
            yield return new WaitForSeconds(12);
            StartCoroutine("Thinking");
        }
    }

    private void SpawnMirages()
    {
        _renderer.material = _mirageMat;
        _animator.Play("Default");

        for (int i = 0; i < _maxMirages; i++)
        {
            GameObject newMirage = Instantiate(_mirage, transform.position, transform.rotation);
            BossMirage mirageScript = newMirage.GetComponent<BossMirage>();
            _mirages.Add(mirageScript);
            mirageScript.realBoss = this;
        }

        _mirageActive = true;
    }

    public void MirageFaded(BossMirage mirageDestroyed)
    {
        _mirages.Remove(mirageDestroyed);
        if (_mirages.Count <= 0) { StartCoroutine("LeaveMirageState"); }
    }

    private IEnumerator LeaveMirageState()
    {
        _mirageActive = false;
        _renderer.material = _normalMat;
        _mirages.Clear();
        yield return new WaitForSeconds(3);
        StartCoroutine("Thinking");
    }

    private void BossKilled(bool playerKilled)
    {
        _bossDead = true;
        _bossCollider.enabled = false;

        if (playerKilled)
        {
            _givePoints.GivePointsToPointManager();
            enemySpawner.IncreaseCurrentLevel();
            GameObject explosion = Instantiate(_bossExplosion, transform.position, transform.rotation);
            explosion.GetComponent<BossExplosion>().boss = this;
        }
        
        GameManager.instance.displayManager.BossHealthDisplay(false);
    }
}
