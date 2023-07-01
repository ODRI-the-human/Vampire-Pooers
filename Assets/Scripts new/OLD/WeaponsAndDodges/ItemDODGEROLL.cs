using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDODGEROLL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NewPlayerMovement>().mouseAltMode = 0;
    }

    void Undo()
    {
        Destroy(this);
    }
}
