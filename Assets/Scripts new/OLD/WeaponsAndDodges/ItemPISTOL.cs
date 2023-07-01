using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPISTOL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<ItemHolder>() != null)
        {
            gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.PISTOL);
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
