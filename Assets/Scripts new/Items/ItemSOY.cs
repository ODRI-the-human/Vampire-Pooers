using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    public bool runStart = true;

    void Start()
    {
        if (!gameObject.GetComponent<DealDamage>().isBulletClone)
        {
            gameObject.GetComponent<DealDamage>().finalDamageDIV += 4;
            gameObject.GetComponent<DealDamage>().knockBackCoeff /= 4;
            gameObject.GetComponent<Attack>().fireTimerDIV += 4;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV -= 4;
        gameObject.GetComponent<DealDamage>().knockBackCoeff *= 4;
        gameObject.GetComponent<Attack>().fireTimerDIV -= 4;

        Debug.Log("Foodland Sale on NOW!!!!!!!!");
    }
}
