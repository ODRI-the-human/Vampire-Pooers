using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERDODGE : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeTimerLength += 3;
        gameObject.GetComponent<NewPlayerMovement>().dodgeSpeedUp += 0.5f;
    }

    public void Undo()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeTimerLength -= 3;
        gameObject.GetComponent<NewPlayerMovement>().dodgeSpeedUp -= 0.5f;
    }
}
