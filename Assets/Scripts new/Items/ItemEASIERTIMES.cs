using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEASIERTIMES : MonoBehaviour
{
    public float instances = 1;

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
