using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemREROLL : MonoBehaviour
{
    public List<int> newItems = new List<int>();
    public int instances = 1;
    GameObject master;
    GameObject guy;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<OtherStuff>().Sprinkle(0);
        if (gameObject.tag == "Player" || gameObject.tag == "MasterObject")
        {
            gameObject.SendMessage("Undo");
            master = EntityReferencerGuy.Instance.master;
            //maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
            Debug.Log("Boingser");
            foreach (int item in gameObject.GetComponent<ItemHolder>().itemsHeld)
            {
                EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = item;
                EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
                int oldItemQuality = EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().quality;
                int itemChosen = (int)ITEMLIST.REROLL;
                int newItemQuality = 0;
                while (itemChosen == (int)ITEMLIST.REROLL || oldItemQuality != newItemQuality)
                {
                    itemChosen = Random.Range(0, (int)ITEMLIST.PISTOL);
                    EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
                    EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
                    newItemQuality = EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().quality;
                }
                Debug.Log("itemChosen: " + itemChosen.ToString() + " / itemQuality: " + newItemQuality.ToString() + " / oldItemQuality: " + oldItemQuality.ToString());
                newItems.Add(itemChosen);
            }

            for (int i = 1; i < instances; i++) // For adding extra items if the player picks up a 3x of this!
            {
                int itemChosen = Random.Range(0, (int)ITEMLIST.PISTOL);
                while (itemChosen == (int)ITEMLIST.REROLL)
                {
                    itemChosen = Random.Range(0, (int)ITEMLIST.PISTOL);
                }
                newItems.Add(itemChosen);
            }

            gameObject.GetComponent<ItemHolder>().itemsHeld = newItems;
            Invoke(nameof(POOPOO), 0.1f);
            Invoke(nameof(KILL), 0.2f);
        }
    }

    public void POOPOO()
    {
        gameObject.GetComponent<ItemHolder>().ApplyAll();
    }

    public void KILL()
    {
        Destroy(this);
    }
}
