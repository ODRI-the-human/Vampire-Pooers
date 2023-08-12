using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/ChestSpawn")]
public class ChestSpawn : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod)
    {
        GameObject gombule = spawnedAttackObjs[0];
        gombule.transform.position = dealer.transform.position;
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
