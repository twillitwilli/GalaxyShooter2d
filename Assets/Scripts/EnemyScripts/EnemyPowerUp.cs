using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUp : MonoBehaviour
{
    [HideInInspector] public Enemy _target;

    private void Update()
    {
        if (_target != null) { transform.position = Vector3.Lerp(transform.position, _target.transform.position, 0.0025f); }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() == _target)
        {
            _target.ActivateShield();
            Destroy(gameObject);
        }
    }
}
