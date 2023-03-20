using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInRadius : MonoBehaviour
{
    GameObject master;

    void Start()
    {
        master = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            master.GetComponent<marcelFunny>().canType = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            master.GetComponent<marcelFunny>().canType = false;
        }
    }
}
