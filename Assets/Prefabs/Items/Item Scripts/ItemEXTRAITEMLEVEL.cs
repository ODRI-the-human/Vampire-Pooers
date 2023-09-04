using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEXTRAITEMLEVEL : ItemScript
{
    int noExtraToGive = 0;

    public override void OnLevel()
    {
        if (gameObject.GetComponent<LevelUp>().level % 4 == 0)
        {
            Debug.Log("No sus jokes, thanks.");
            gameObject.GetComponent<ItemHolder2>().noToGive += 1 * instances;
        }
    }
}
