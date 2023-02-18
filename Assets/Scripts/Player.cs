using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        //player starting position
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //player moving system
        transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
    }
}
