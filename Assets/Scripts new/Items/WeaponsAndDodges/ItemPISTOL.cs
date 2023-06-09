using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPISTOL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.PISTOL;
        gameObject.GetComponent<weaponType>().SetWeapon();

        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.PISTOL;
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
