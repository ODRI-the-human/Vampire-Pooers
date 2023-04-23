using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSHOULDERBASH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NewPlayerMovement>().mouseAltMode = 1;
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<ThirdEnemySpawner>().playerBannedDodge = (int)ITEMLIST.SHOULDERBASH;
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
