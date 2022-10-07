using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV += 4;
        gameObject.GetComponent<DealDamage>().knockBackCoeff /= 4;
        gameObject.GetComponent<Attack>().fireTimerDIV += 4;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV -= 4;
        gameObject.GetComponent<DealDamage>().knockBackCoeff *= 4;
        gameObject.GetComponent<Attack>().fireTimerDIV -= 4;

        Debug.Log("Foodland Sale on NOW!!!!!!!!");
    }
}
