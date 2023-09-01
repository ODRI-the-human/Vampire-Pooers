using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBLEED : MonoBehaviour
{
    public int instances = 1;
    float pringle;
    float procMoment;
    float iFrameLength = 0;

    public bool overrideRoll = false;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void RollOnHit(GameObject[] objects)
    {
        GameObject victim = objects[0];
        GameObject source = objects[1];

        Component[] components = gameObject.GetComponents(typeof(Component));
        int scriptIndex = System.Array.IndexOf(components, this);

        int numEffects = 0;
        if (overrideRoll)
        {
            numEffects = 1;
        }
        else
        {
            numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(15 * instances, source, scriptIndex);
        }

        for (int i = 0; i < numEffects; i++)
        {
            victim.GetComponent<Statuses>().AddStatus((int)STATUSES.BLEED, 0, gameObject);
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
