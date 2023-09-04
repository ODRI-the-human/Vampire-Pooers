using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOMINGFAMILIAR : ItemScript
{
    public override void AddStack()
    {
        if (gameObject.GetComponent<gunnerManagement>() == null)
        {
            gameObject.AddComponent<gunnerManagement>();
        }
        gameObject.GetComponent<gunnerManagement>().AddNew((int)ITEMLIST.HOMINGFAMILIAR);
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<gunnerManagement>().RemoveGunner((int)ITEMLIST.HOMINGFAMILIAR);
    }
}
