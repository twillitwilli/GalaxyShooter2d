using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed;

    private void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (transform.position.y > 10) { Destroy(gameObject); }
    }
}
