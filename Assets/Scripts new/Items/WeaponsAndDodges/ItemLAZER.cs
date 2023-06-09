using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLAZER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<weaponType>() != null)
        {
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.LAZER;
            gameObject.GetComponent<weaponType>().SetWeapon();
        }

        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.LAZER;
        }
    }

    // Update is called once per frame
    void Undo()
    {
        Destroy(this);
    }
}
