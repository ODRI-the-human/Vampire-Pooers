using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For general weapons that don't need any extra sauce added, like enemies' homing mines and whatever.
[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/MiscParams")]
public class MiscParams : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag)
    {
        // nothing please get fricked up buddy!
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
