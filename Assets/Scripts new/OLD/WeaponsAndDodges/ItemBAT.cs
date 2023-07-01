using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBAT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<ItemHolder>() != null)
        {
            gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.BAT);
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
