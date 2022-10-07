using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERDODGE : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeUp++;
    }

    public void Undo()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeUp--;
    }
}
