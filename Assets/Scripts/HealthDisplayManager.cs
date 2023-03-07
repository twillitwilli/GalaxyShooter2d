using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _healthImages;

    public void UpdateHealthDisplay(int currentHealth)
    {
        _image.sprite = _healthImages[currentHealth];
    }
}
