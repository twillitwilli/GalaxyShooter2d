using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(movementDirection * player.playerSpeed * Time.deltaTime);
        CheckPlayerBounds();
    }

    private void CheckPlayerBounds()
    {
        //limit for vertical movement
        if (transform.position.y >= 5) { transform.position = new Vector3(transform.position.x, 5, 0); }
        else if (transform.position.y <= -5.5f) { transform.position = new Vector3(transform.position.x, -5.5f, 0); }
        //limit for horizontal movement
        if (transform.position.x >= 10) { transform.position = new Vector3(10, transform.position.y, 0); }
        else if (transform.position.x <= -10) { transform.position = new Vector3(-10, transform.position.y, 0); }
    }
}
