using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth, playerSpeed, fireRate, damage;
    [HideInInspector] public float currentHealth;
    public GameObject laserProjectile;
    [HideInInspector] public bool playerDead;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void AdjustHealth(float healthValue)
    {
        currentHealth += healthValue;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        else if (currentHealth < 0) { playerDead = true; }
    }
}
