using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<DealDamage>().damageMult /= 4;
        gameObject.GetComponent<Attack>().fireTimerLength /= 5;
    }
}
