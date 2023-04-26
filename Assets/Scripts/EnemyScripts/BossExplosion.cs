using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    [HideInInspector] public Boss boss;

    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        StartCoroutine("BossExploding");
    }

    private IEnumerator BossExploding()
    {
        int randomExplosionCount = Random.Range(15, 20);
        for (int i = 0; i < randomExplosionCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
            float xPos = transform.position.x + (Random.Range(-0.8f, 0.8f));
            float yPos = transform.position.y + (Random.Range(-0.9f, 0.9f));
            GameObject newExplosion = Instantiate(_explosion, new Vector3(xPos, yPos, -1), transform.rotation);

            float randomSize = Random.Range(0.25f, 0.5f);
            newExplosion.transform.localScale = new Vector3(randomSize, randomSize, 1);
        }

        GameObject finalExplosion = Instantiate(_explosion, transform.position, transform.rotation);
        GameManager.instance.cameraController.ShakeCamera();
        finalExplosion.transform.localScale = new Vector3(1, 1, 1);

        StartCoroutine("BossDestroyed");
    }

    private IEnumerator BossDestroyed()
    {
        yield return new WaitForSeconds(0.5f);
        if (boss != null) { Destroy(boss.gameObject); }
    }
}
