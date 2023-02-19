using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;

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
