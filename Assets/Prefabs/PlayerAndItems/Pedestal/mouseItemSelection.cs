using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseItemSelection : MonoBehaviour
{
    public GameObject master;
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
            }
        }
        Debug.Log("position: " + transform.position.ToString());//"selected pedestal: " + selectedPedestal.name);

        if (selectedPedestal.GetComponent<itemPedestal>() != null)
        {
            selectedPedestal.GetComponent<itemPedestal>().GiveDaItem(master);
            master.GetComponent<ItemHolder>().GiveFunny(selectedPedestal);
        }
        else
        {
            selectedPedestal.GetComponent<WeaponPedestal>().StartPickup(master);
        }
    }
}
