using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBOUNCY : MonoBehaviour
{

    public int instances = 1;
    public int bouncesLeft;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        bouncesLeft = instances;
    }

    public void Undo()
    {
        Destroy(this);
    }
}
