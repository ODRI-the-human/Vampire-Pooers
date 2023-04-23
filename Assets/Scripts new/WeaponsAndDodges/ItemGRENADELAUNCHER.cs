using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGRENADELAUNCHER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.GRENADELAUNCHER;
        gameObject.GetComponent<weaponType>().SetWeapon();

        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.GRENADELAUNCHER;
        }
    }

    // Update is called once per frame
    void Undo()
    {
        Destroy(this);
    }
}
