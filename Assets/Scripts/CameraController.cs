using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShakeCamera()
    {
        _animator.SetBool("ShakeCamera", true);
    }

    public void CameraIdle()
    {
        _animator.SetBool("ShakeCamera", false);
    }
}
