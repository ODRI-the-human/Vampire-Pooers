using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERCRITS : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<DealDamage>().critMult += 1;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().critMult -= 1;
        Destroy(this);
    }
}
