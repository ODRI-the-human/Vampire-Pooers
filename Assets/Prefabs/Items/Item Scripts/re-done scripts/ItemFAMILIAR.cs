using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFAMILIAR : ItemScript
{
    public override void AddStack()
    {
        if (gameObject.GetComponent<gunnerManagement>() == null)
        {
            gameObject.AddComponent<gunnerManagement>();
        }
        gameObject.GetComponent<gunnerManagement>().AddNew((int)ITEMLIST.FAMILIAR);
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<gunnerManagement>().RemoveGunner((int)ITEMLIST.FAMILIAR);
    }
}
