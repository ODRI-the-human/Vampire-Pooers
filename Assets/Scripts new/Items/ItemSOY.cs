using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<DealDamage>().finalDamageMult /= 4;
        gameObject.GetComponent<DealDamage>().knockBackCoeff /= 4;
        gameObject.GetComponent<Attack>().fireTimerLength /= 5;
    }
}
