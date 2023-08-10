using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/CrossbowBall")]
public class CrossBowBallAttack : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod)
    {
        GameObject Harrybo = spawnedAttackObjs[0];
        Harrybo.GetComponent<CrossbowBall>().direction = direction;
        Harrybo.GetComponent<CrossbowBall>().owner = dealer;
        //Debug.Log("transform.up for lazer: " + Harrybo.transform.up.ToString() + " / direction: " + direction.ToString());

        //if (!isPlayerTeam)
        //{
        //    Debug.Log("changed delay ok");
        //    Harrybo.GetComponent<checkAllLazerPositions>().delay = 0.5f;
        //}
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
