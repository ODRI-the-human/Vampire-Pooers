using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemREROLL : MonoBehaviour
{
    public List<int> newItems = new List<int>();
    int maxRange;
    int minRange = 0;
    GameObject master;
    GameObject guy;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<OtherStuff>().Sprinkle(0);
        master = GameObject.Find("bigFuckingMasterObject");
        maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
        Debug.Log("Boingser");
        foreach (int item in gameObject.GetComponent<ItemHolder>().itemsHeld)
        {
            int itemChosen = Random.Range(minRange, maxRange);
            newItems.Add(itemChosen);
        }
        gameObject.GetComponent<ItemHolder>().itemsHeld = newItems;
        SendMessage("Undo");
        Invoke(nameof(POOPOO),0.1f);
    }

    public void POOPOO()
    {
        gameObject.GetComponent<ItemHolder>().ApplyAll();
    }

    public void Undo()
    {
        //nothin
    }
}
