using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/LightningShot")]
public class LightningShot : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag)
    {
        Vector3 vecToTarget = new Vector3(direction.x, direction.y, 0);
        GameObject Harrybo = spawnedAttackObjs[0];
        Harrybo.transform.localScale = new Vector3(1, 1, 1);//150, 1);
        Harrybo.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 90); //90 + 180 * noExtraShots + 
        Harrybo.GetComponent<checkAllLazerPositions>().owner = dealer;
        //Debug.Log(vecToTarget.ToString());

        Harrybo.GetComponent<ItemHolder>().itemsHeld = dealer.GetComponent<ItemHolder>().itemsHeld;
        Harrybo.GetComponent<DealDamage>().owner = dealer;
        Harrybo.GetComponent<checkAllLazerPositions>().master = dealer.GetComponent<DealDamage>().master;

        if (!isPlayerTeam)
        {
            Debug.Log("changed delay ok");
            Harrybo.GetComponent<checkAllLazerPositions>().delay = 0.5f;
        }
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
