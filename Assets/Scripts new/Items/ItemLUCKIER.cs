using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLUCKIER : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!gameObject.GetComponent<DealDamage>().isBulletClone)
        {
            gameObject.GetComponent<DealDamage>().procCoeff += 1;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().procCoeff -= 1;
    }
}