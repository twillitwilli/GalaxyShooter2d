using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    private float randomSpeed;

    private void Start()
    {
        randomSpeed = Random.Range(0.25f, 1.5f);
    }

    private void Update()
    {
        transform.Translate(-Vector3.up * randomSpeed * Time.deltaTime);
        if (transform.position.y < -8) { Destroy(gameObject); }
    }
}
