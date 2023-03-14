using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _seconds;

    private void OnEnable()
    {
        StartCoroutine("DisableGameobject");
    }

    private IEnumerator DisableGameobject()
    {
        yield return new WaitForSeconds(_seconds);
        gameObject.SetActive(false);
    }
}
