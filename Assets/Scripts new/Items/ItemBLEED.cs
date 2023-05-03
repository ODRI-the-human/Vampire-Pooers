using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBLEED : MonoBehaviour
{
    public int instances = 1;
    float pringle;
    float procMoment;
    float iFrameLength = 0;

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

        int numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(15 * instances, source, scriptIndex);
        for (int i = 0; i < numEffects; i++)
        {
            victim.GetComponent<Statuses>().bleedStacks += 1;
            victim.GetComponent<Statuses>().bleedTimer = 0;
            if (!victim.GetComponent<Statuses>().iconOrder.Contains(0))
            {
                victim.GetComponent<Statuses>().iconOrder.Add(0);
            }
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
