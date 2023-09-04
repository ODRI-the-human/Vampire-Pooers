using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<Attack>().cooldownFacIndiv[0] *= 0.9f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<Attack>().cooldownFacIndiv[0] /= 0.9f;
    }
}
