using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPickerUpper : MonoBehaviour
{
    public GameObject owner;

    void Update()
    {
        transform.position = owner.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP" && !col.gameObject.GetComponent<XPMoveTowardsPlayer>().taken)
        {
            col.gameObject.GetComponent<XPMoveTowardsPlayer>().taken = true;
            col.gameObject.GetComponent<XPMoveTowardsPlayer>().timer = 0;
            col.gameObject.GetComponent<XPMoveTowardsPlayer>().target = owner;
        }
    }
}
