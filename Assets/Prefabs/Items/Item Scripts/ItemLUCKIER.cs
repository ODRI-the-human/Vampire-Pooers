using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLUCKIER : MonoBehaviour
{
    public int instances = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetVal();
    }

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
            SetVal();
        }
    }

    void SetVal()
    {
        float increaseRate = 5; // Increase this to reduce the rate at which this item scales.
        float bonusAmt = 2; // Increase this to make the item give you a larger bonus (and higher asymptote)
        gameObject.GetComponent<DealDamage>().procChanceBonus = -bonusAmt * (2 * increaseRate / (instances + increaseRate) - 3) - (bonusAmt - 1);
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().procChanceBonus = 1;
        Destroy(this);
    }
}
