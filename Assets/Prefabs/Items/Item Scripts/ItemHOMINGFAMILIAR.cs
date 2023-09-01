using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOMINGFAMILIAR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<gunnerManagement>() == null)
        {
            gameObject.AddComponent<gunnerManagement>();
        }
        gameObject.GetComponent<gunnerManagement>().AddNew((int)ITEMLIST.HOMINGFAMILIAR);
    }

    void Undo()
    {
        Destroy(this);
    }
}
