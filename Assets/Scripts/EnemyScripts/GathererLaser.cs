using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererLaser : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(-Vector3.up * 10 * Time.deltaTime);
        if (transform.position.y < -10) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meteor meteor;
        if (collision.gameObject.TryGetComponent<Meteor>(out meteor))
        {
            meteor.exploded = true;
            Destroy(meteor.gameObject);
        }
    }
}
