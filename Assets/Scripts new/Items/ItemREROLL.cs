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
        //gameObject.GetComponent<OtherStuff>().Sprinkle(0);
        if (gameObject.tag == "Player" || gameObject.tag == "MasterObject")
        {
            gameObject.SendMessage("Undo");
            master = EntityReferencerGuy.Instance.master;
            maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
            Debug.Log("Boingser");
            foreach (int item in gameObject.GetComponent<ItemHolder>().itemsHeld)
            {
                int itemChosen = Random.Range(minRange, maxRange);
                while (itemChosen == (int)ITEMLIST.REROLL)
                {
                    itemChosen = Random.Range(minRange, maxRange);
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
        GameObject player = EntityReferencerGuy.Instance.playerInstance;
        if (gameObject.tag == "Player")
        {
            player.GetComponent<ItemHolder>().ApplyAll();
        }
    }

    public void KILL()
    {
        Destroy(this);
    }
}
