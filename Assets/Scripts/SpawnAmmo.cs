using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    [SerializeField] private GameObject _ammoCollectable;

    private void OnDestroy()
    {
        Instantiate(_ammoCollectable, transform.position, transform.rotation);
    }
}
