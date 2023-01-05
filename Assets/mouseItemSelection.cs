using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseItemSelection : MonoBehaviour
{
    public GameObject master;

    void Start()
    {
        Invoke(nameof(deathMoment), 0.1f);
    }

    void deathMoment()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "item")
        {
            col.gameObject.GetComponent<itemPedestal>().GiveDaItem(master);
            master.GetComponent<ItemHolder>().GiveFunny(col.gameObject);
        }
    }
}
