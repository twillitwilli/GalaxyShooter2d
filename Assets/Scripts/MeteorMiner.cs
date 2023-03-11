using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMiner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.Gathered();
        }
    }
}
