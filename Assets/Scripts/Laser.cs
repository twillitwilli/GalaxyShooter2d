using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public Player player;
    private float _laserSpeed = 10;
    [SerializeField] private GameObject _impact;

    private void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy;
        Meteor meteor;
        Boss boss;
        BossMirage mirage;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            enemy.AdjustHealth(-player.playerStats.GetAttackDamage());
            Impact();
        }
        else if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.Destroyed();
            Impact();
        }
        else if (collision.gameObject.TryGetComponent<Boss>(out boss))
        {
            boss.AdjustBossHealth(-player.playerStats.GetAttackDamage());
            Impact();
        }
        else if (collision.gameObject.TryGetComponent<BossMirage>(out mirage))
        {
            mirage.AdjustCurrentHealth(-1);
        }
    }

    private void Impact()
    {
        GameObject newImpact = Instantiate(_impact, transform.position, transform.rotation);
        newImpact.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        Destroy(gameObject);
    }
}
