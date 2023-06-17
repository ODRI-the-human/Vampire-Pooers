using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityTest1 : MonoBehaviour
{
    public int abilityNo = 2; // 1 for ability 1, 2 for ability 2.
    public float abilityTimerMax;

    void Start()
    {
        abilityTimerMax = 600;

        switch (abilityNo)
        {
            case 1:
                gameObject.GetComponent<secondaryAbility>().abilityOneMaxCooldown = abilityTimerMax;
                break;
            case 2:
                gameObject.GetComponent<secondaryAbility>().abilityTwoMaxCooldown = abilityTimerMax;
                break;
        }
    }

    void UseAbility(int which)
    {
        if (which == abilityNo)
        {
            //Instantiate(EntityReferencerGuy.Instance.Creep);
        }
    }
}
