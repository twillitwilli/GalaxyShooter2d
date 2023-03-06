using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds = 3;

    private void Start()
    {
        StartCoroutine("DestroyObject");
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
