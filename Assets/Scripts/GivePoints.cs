using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePoints : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private int _pointValue;

    public void GivePointsToPointManager()
    {
        GameManager.instance.displayManager.UpdateCurrentScore(_pointValue);
    }
}
