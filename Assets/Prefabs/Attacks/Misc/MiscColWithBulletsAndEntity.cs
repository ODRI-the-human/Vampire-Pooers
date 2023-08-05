using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For general weapons that don't need any extra sauce added, like enemies' homing mines and whatever.
[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/MiscAlsoCollideWithBullets")]
public class MiscColWithBulletsAndEntity : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod)
    {
        GameObject objoct = spawnedAttackObjs[0];
        if (isPlayerTeam)
        {
            objoct.layer = LayerMask.NameToLayer("HitEnemBulletsAndEnemies");
        }
        else
        {
            objoct.layer = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
        }
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
