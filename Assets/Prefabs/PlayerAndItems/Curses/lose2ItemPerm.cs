using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose2ItemPerm : MonoBehaviour
{
    public int numItemsToLose = 2;
    public List<int> itemsToGiveOnRoundStart = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnHurtEffects()
    {
        gameObject.SendMessage("Undo");
        for (int i = 0; i < numItemsToLose; i++)
        {
            if (gameObject.GetComponent<ItemHolder>().itemsHeld.Count > 0)
            {
                int itemIndex = Random.Range(0, gameObject.GetComponent<ItemHolder>().itemsHeld.Count);
                gameObject.GetComponent<ItemHolder>().itemsHeld.RemoveAt(itemIndex);
            }
        }

        gameObject.GetComponent<ItemHolder>().ApplyAll();
    }

    public void itemsAdded(bool isPassive)
    {
        if (isPassive)
        {
            foreach (int item in itemsToGiveOnRoundStart)
            {
                gameObject.GetComponent<ItemHolder>().itemsHeld.Add(item);
                gameObject.GetComponent<ItemHolder>().itemGained = item;
                gameObject.GetComponent<ItemHolder>().ApplyItems();
            }
        }
    }
}
