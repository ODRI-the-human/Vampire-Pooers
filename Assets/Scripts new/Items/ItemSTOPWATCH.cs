using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSTOPWATCH : MonoBehaviour
{
    float amountLol = 1.2f;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt *= amountLol;
        }
        else
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt /= amountLol;
        }
    }

    public void Undo()
    {
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt /= amountLol;
        }
        else
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt *= amountLol;
        }
        Destroy(this);
    }
}
