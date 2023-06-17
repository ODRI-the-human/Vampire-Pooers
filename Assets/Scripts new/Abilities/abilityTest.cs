using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityTest : MonoBehaviour
{
    public int abilityNo = 1; // 1 for ability 1, 2 for ability 2.
    public float abilityTimerMax;

    void Start()
    {
        abilityTimerMax = 150;

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
            //Instantiate(EntityReferencerGuy.Instance.ATGMissile);
        }
    }
}
