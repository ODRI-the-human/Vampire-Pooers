using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieIfOwnerDies : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<DealDamage>().owner == null)
        {
            Destroy(gameObject);
        }
    }
}
