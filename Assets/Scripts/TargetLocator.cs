using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private HomingMissile homingMissile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!homingMissile.HasTarget())
        {
            Enemy enemy;
            Boss boss;

            if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
            {
                homingMissile.NewTargetFound(enemy.transform);
            }
            else if (collision.gameObject.TryGetComponent<Boss>(out boss))
            {
                homingMissile.NewTargetFound(boss.transform);
            }
        }
    }
}
