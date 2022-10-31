using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRANDOMSHOT : MonoBehaviour
{
    int itemChosen;
    GameObject master;
    int numItemsExist;
    bool canBeUsed;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Bullet_Movement>() != null)
        {
            master = gameObject.GetComponent<Bullet_Movement>().master;
            numItemsExist = master.GetComponent<EntityReferencerGuy>().numItemsExist;
            canBeUsed = false;
            while (!canBeUsed)
            {
                itemChosen = Random.Range(0, numItemsExist);
                master.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
                master.GetComponent<ItemDescriptions>().getItemDescription();
                canBeUsed = master.GetComponent<ItemDescriptions>().applyToBullets;
            }
            gameObject.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
            gameObject.GetComponent<ItemHolder>().itemGained = itemChosen;
            gameObject.GetComponent<ItemHolder>().ApplyItems();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
