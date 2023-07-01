using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItemOnHit : MonoBehaviour
{
    public GameObject itemPedestal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ApplyOwnOnDeaths()
    {
        float doesTheFunny = Random.Range(0, 100f);

        if (doesTheFunny > 95f)
        {
            GameObject pedestal = Instantiate(itemPedestal, transform.position, Quaternion.Euler(0, 0, 0));
            pedestal.GetComponent<itemPedestal>().doRandomiseItem = false;

            int numItemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld.Count;
            int itemToGive = gameObject.GetComponent<ItemHolder>().itemsHeld[Random.Range(0, numItemsHeld)];

            Debug.Log("Item to give: " + itemToGive.ToString());

            pedestal.GetComponent<itemPedestal>().itemChosen = itemToGive;
        }
    }
}
