using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSTOPWATCH : ItemScript
{
    float amountLol = 1.2f;

    public override void AddStack()
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

    public override void RemoveStack()
    {
        if (gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt /= amountLol;
        }
        else
        {
            EntityReferencerGuy.Instance.stopWatchDebuffAmt *= amountLol;
        }
    }
}
