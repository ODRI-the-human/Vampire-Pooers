using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLUCKIER : MonoBehaviour
{
    float bonus;

    // Start is called before the first frame update
    void Awake()
    {
        bonus = (1 / gameObject.GetComponent<DealDamage>().procCoeff);
        gameObject.GetComponent<DealDamage>().procCoeff += bonus;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().procCoeff -= bonus;
        Destroy(this);
    }
}
