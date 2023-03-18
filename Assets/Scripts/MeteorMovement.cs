using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    private float _randomSpeed;

    private void Start()
    {
        _randomSpeed = Random.Range(0.25f, 1.5f);
    }

    private void Update()
    {
        transform.Translate(-Vector3.up * _randomSpeed * Time.deltaTime);
        if (transform.position.y < -8) 
        {
            GetComponentInChildren<Meteor>().leftScreen = true;
            Destroy(gameObject);
        }
    }
}
