using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Player player;
        if (other.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
        }

        Meteor meteor;
        if (other.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.exploded = true;
            Destroy(meteor.gameObject);
        }
    }
}
