using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLAZER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<ItemHolder>() != null)
        {
            gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.LAZER);
        }
    }

    // Update is called once per frame
    void Undo()
    {
        Destroy(this);
    }
}
