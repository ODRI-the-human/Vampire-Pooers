using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSTOPWATCH : MonoBehaviour
{
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
