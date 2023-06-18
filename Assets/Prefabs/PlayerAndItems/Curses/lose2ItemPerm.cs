using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose2ItemPerm : MonoBehaviour
{
    public int numItemsToLose = 2;
    public List<int> itemsToGiveOnRoundStart = new List<int>();
    int numItemsPickedUp = -1;

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

        Invoke(nameof(ApplyItems), 0.001f);
    }

    void ApplyItems()
    {
        gameObject.GetComponent<ItemHolder>().ApplyAll();
    }

    public void itemsAdded(bool isPassive)
    {
        numItemsPickedUp++;
        if (isPassive && numItemsPickedUp % 3 == 0 && numItemsPickedUp > 0)
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
