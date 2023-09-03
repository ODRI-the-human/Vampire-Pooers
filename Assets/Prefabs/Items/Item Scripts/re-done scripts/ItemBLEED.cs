using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBLEED : ItemScript
{
    float pringle;
    float procMoment;
    float iFrameLength = 0;

    public bool overrideRoll = false;

    public override void OnHit(GameObject victim, GameObject responsible)
    {
        Component[] components = gameObject.GetComponents(typeof(Component));
        int scriptIndex = System.Array.IndexOf(components, this);

        int numEffects = 0;
        if (overrideRoll)
        {
            numEffects = 1;
        }
        else
        {
            numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(15 * instances, responsible, scriptIndex);
        }

        for (int i = 0; i < numEffects; i++)
        {
            victim.GetComponent<Statuses>().AddStatus((int)STATUSES.BLEED, 0, gameObject);
        }
    }
}
