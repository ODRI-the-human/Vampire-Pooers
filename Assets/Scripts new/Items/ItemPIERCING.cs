using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : MonoBehaviour
{
    public int instances = 1;
    public int piercesLeft;

    void Start()
    {
        piercesLeft = instances;
    }

    public void Undo()
    {
        //nothin
    }
}
