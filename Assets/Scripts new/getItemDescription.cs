using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getItemDescription : MonoBehaviour
{
    public bool itemsExist = false;
    int itemNearest = 0;
    public string itemDescription;

    // Update is called once per frame
    void Update()
    {
        if (itemsExist)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("item");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            itemDescription = closest.GetComponent<itemPedestal>().description;

        }
        else
        {
            itemDescription = "poop malam";
        }
    }
}
