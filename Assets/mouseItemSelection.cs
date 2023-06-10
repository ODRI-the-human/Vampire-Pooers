using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseItemSelection : MonoBehaviour
{
    public GameObject master;

    void Start()
    {
        GameObject[] pedestals = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject pedestal in pedestals)
        {
            Vector3 posDiff = pedestal.transform.position - transform.position;
            posDiff = new Vector3(posDiff.x, posDiff.y, 0);
            if (posDiff.magnitude < 1f)
            {
                pedestal.GetComponent<itemPedestal>().GiveDaItem(master);
                master.GetComponent<ItemHolder>().GiveFunny(pedestal);
            }
        }

        Invoke(nameof(deathMoment), 0.1f);
    }

    void deathMoment()
    {
        Destroy(gameObject);
    }
}
