using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEASIERTIMES : MonoBehaviour
{
    public float instances = 1;

    public void Undo()
    {
        Destroy(this);
    }
}
