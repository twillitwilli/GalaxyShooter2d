using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5;
    public GameObject laserProjectile;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
