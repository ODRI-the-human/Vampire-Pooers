using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBOUNCY : MonoBehaviour
{

    public int instances;
    public int bouncesLeft;

    void Start()
    {
        instances = 1;
        bouncesLeft = instances;
    }
}
