using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEXTRAITEMLEVEL : MonoBehaviour
{
    public int instances = 1;
    int noExtraToGive = 0;

    public void LevelEffects()
    {
        if (gameObject.GetComponent<LevelUp>().level % 4 == 0)
        {
            Debug.Log("No sus jokes, thanks.");
            gameObject.GetComponent<ItemHolder>().noToGive += 1 * instances;
        }
    }

    public void itemsAdded(bool isPassive)
    {
        if (isPassive)
        {
            gameObject.GetComponent<ItemHolder>().noToGive = 1; // Only resets the number to add if the player chooses a passive item.
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
