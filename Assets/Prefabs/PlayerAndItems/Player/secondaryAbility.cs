using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class secondaryAbility : MonoBehaviour
{
    public float abilityOneCooldown = 0;
    public float abilityOneMaxCooldown = 1;
    public float abilityTwoCooldown = 0;
    public float abilityTwoMaxCooldown = 1;


    public void useAbilityOne(InputAction.CallbackContext context)
    {
        if (abilityOneCooldown <= 0)
        {
            TriggerAbility(1);
        }
    }

    public void useAbilityTwo(InputAction.CallbackContext context)
    {
        if (abilityTwoCooldown <= 0)
        {
            TriggerAbility(2);
        }
    }

    void TriggerAbility(int which)
    {
        SendMessage("UseAbility", which);

        switch (which)
        {
            case 1:
                abilityOneCooldown = abilityOneMaxCooldown;
                break;
            case 2:
                abilityTwoCooldown = abilityTwoMaxCooldown;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        abilityOneCooldown--;
        abilityTwoCooldown--;
    }
}
