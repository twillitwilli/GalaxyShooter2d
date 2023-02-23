using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _playerRenderer;
    [SerializeField] private GameObject _playerExplosion;
    public GameObject laserProjectile;
    public float maxHealth, playerSpeed, fireRate, damage;
    [HideInInspector] public float currentHealth;

    private void Start()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
        transform.localPosition = new Vector3(0, 0, 0);
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().AdjustHealth(-damage * 2);
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
        _playerRenderer.color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);
        _playerRenderer.color = new Color(255, 255, 255, 255);
    }

    private void PlayerDied()
    {
        GameObject newExplosion = Instantiate(_playerExplosion, transform.position, transform.rotation);
        newExplosion.transform.SetParent(null);
        FindObjectOfType<EnemySpawner>().disableSpawner = true;
        Destroy(gameObject);
    }
}
