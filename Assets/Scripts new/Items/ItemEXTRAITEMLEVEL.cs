using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEXTRAITEMLEVEL : MonoBehaviour
{
    public int instances = 1;
    int noExtraToGive = 0;

    // Start is called before the first frame update
    void Start()
    {
        LevelUp.levelEffects += giveExtraItems;
    }

    public void giveExtraItems()
    {
        if (gameObject.GetComponent<LevelUp>().level % 4 == 0)
        {
            Debug.Log("No sus jokes, thanks.");
            gameObject.GetComponent<ItemHolder>().noToGive += 1 * instances;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            gameObject.GetComponent<ItemHolder>().noToGive = 1;
        }
    }

    public void Undo()
    {
        LevelUp.levelEffects -= giveExtraItems;
        Destroy(this);
    }
}
