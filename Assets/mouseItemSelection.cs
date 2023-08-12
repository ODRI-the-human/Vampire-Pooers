using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseItemSelection : MonoBehaviour
{
    public GameObject master;
    public float timer = 0;
    GameObject selectedPedestal;

    void Start()
    {
        GameObject[] pedestals = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject pedestal in pedestals)
        {
            Vector3 posDiff = pedestal.transform.position - transform.position;
            posDiff = new Vector3(posDiff.x, posDiff.y, 0);
            if (posDiff.magnitude < 1f)
            {
                selectedPedestal = pedestal;
                if (pedestal.transform.parent != null)
                {

                }
            }
        }
    }

    void Update()
    {
        if (timer >= 25)
        {
            selectedPedestal.GetComponent<itemPedestal>().GiveDaItem(master);
            master.GetComponent<ItemHolder>().GiveFunny(selectedPedestal);
        }
        timer += 50 * Time.deltaTime;
    }
}
