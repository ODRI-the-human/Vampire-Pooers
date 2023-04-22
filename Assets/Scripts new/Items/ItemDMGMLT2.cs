using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGMLT2 : MonoBehaviour
{
    public bool runStart = true;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<DealDamage>().damageMult += 1f;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().damageMult -= 1f;
        Destroy(this);
    }
}
