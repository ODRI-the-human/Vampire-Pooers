using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : MonoBehaviour
{
    public int instances = 1;
    public int piercesLeft;
    public bool giveDamageBonus = true;

    void Start()
    {
        piercesLeft = instances;
    }

    public void Undo()
    {
        //nothin
    }
}
