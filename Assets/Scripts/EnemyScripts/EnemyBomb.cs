using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    private float _speed;
    [SerializeField] private AudioClip explosionSFX;

    private void Start()
    {
        _speed = Random.Range(2.5f, 5);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine("StopBomb");
    }

    private void Update()
    {
        transform.Translate(-Vector3.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.playerStats.AdjustCurrentHealth(-1);
        }
    }

    private IEnumerator StopBomb()
    {
        yield return new WaitForSeconds(1.5f);
        _speed = 0;
    }

    public void BombExploded()
    {
        GameManager.instance.cameraController.ShakeCamera();
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
    }
}
