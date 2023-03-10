using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORELEVELSTATS : MonoBehaviour
{
    public int instances = 1;

    public void Undo()
    {
        Destroy(this);
    }
}
