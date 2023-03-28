using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpin : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        int randomizeDirection = Random.Range(0, 100);
        if (randomizeDirection > 50) { _animator.Play("LaserSpinRight"); }
        else { _animator.Play("LaserSpinLeft"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
        }

        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            Destroy(meteor.gameObject);
        }
    }
}
