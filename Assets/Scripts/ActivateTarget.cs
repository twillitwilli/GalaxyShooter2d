using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTarget : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float secondsToActive;

    private void Start()
    {
        StartCoroutine("Activate");
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(secondsToActive);
        _target.SetActive(true);
    }
}
