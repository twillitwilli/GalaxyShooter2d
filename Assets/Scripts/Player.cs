using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _playerRenderer;
    [SerializeField] private GameObject _playerExplosion;
    public float maxHealth, playerSpeed, fireRate, damage;
    [HideInInspector] public float currentHealth;
    private WaitForSeconds _colorChangeWaitTime = new WaitForSeconds(0.2f);
    private Color _defaualtColor = new Color(255, 255, 255, 255);
    private Color _playerHitColor = new Color(255, 0, 0, 255);

    private void Start()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
        transform.localPosition = new Vector3(0, 0, 0);
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.AdjustHealth(-damage * 2);
            AdjustHealth(-25);
        }
    }

    public void AdjustHealth(float healthValue)
    {
        if (healthValue < 0) { StartCoroutine("PlayerHitColorChange"); }
        currentHealth += healthValue;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        else if (currentHealth < 0) { PlayerDied(); }
    }

    private IEnumerator PlayerHitColorChange()
    {
        _playerRenderer.color = _playerHitColor;
        yield return _colorChangeWaitTime;
        _playerRenderer.color = _defaualtColor;
    }

    private void PlayerDied()
    {
        Instantiate(_playerExplosion, transform.position, transform.rotation);
        FindObjectOfType<EnemySpawner>().disableSpawner = true;
        Destroy(gameObject);
    }
}
