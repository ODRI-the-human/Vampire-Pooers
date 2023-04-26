using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSTOPWATCH : MonoBehaviour
{
    float amountLol = 1.3f;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<MasterItemManager>().stopWatchInstances *= amountLol;
        }
        else
        {
            EntityReferencerGuy.Instance.master.GetComponent<MasterItemManager>().stopWatchInstances /= amountLol;
        }
    }

    public void Undo()
    {
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<MasterItemManager>().stopWatchInstances *= amountLol;
        }
        else
        {
            EntityReferencerGuy.Instance.master.GetComponent<MasterItemManager>().stopWatchInstances /= amountLol;
        }
        Destroy(this);
    }
}
