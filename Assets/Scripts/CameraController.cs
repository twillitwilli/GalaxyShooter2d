using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShakeCamera()
    {
        animator.SetBool("ShakeCamera", true);
    }

    public void CameraIdle()
    {
        animator.SetBool("ShakeCamera", false);
    }
}
