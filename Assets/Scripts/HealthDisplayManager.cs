using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] healthImages;

    public void UpdateHealthDisplay(int currentHealth)
    {
        image.sprite = healthImages[currentHealth];
    }
}
