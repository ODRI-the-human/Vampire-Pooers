using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGRENADELAUNCHER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.GRENADELAUNCHER);
    }

    // Update is called once per frame
    void Undo()
    {
        Destroy(this);
    }
}
