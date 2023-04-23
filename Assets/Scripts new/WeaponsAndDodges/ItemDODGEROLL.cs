using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDODGEROLL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NewPlayerMovement>().mouseAltMode = 0;
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<ThirdEnemySpawner>().playerBannedDodge = (int)ITEMLIST.DODGEROLL;
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
