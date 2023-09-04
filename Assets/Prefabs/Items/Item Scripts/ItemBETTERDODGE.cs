using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERDODGE : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeTimerLength += 3;
        gameObject.GetComponent<NewPlayerMovement>().dodgeSpeedUp += 0.5f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<NewPlayerMovement>().dodgeTimerLength -= 3;
        gameObject.GetComponent<NewPlayerMovement>().dodgeSpeedUp -= 0.5f;
    }
}
