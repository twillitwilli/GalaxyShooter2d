using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeChance : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up * 20 * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OffensiveEnemy enemy;
        if (collision.gameObject.TryGetComponent<OffensiveEnemy>(out enemy))
        {
            enemy.DodgeChance();
            Destroy(gameObject);
        }
    }
}
