using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/MeleeBasic")]
public class MeleeBase : AbilityParams
{
    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag)
    {
        //bool isThisAttackCharged = isChargedAttack;

        GameObject hitBox = spawnedAttackObjs[0];
        Debug.Log("funny melee moment: " + hitBox.ToString() + " / is charged? " + isCharged.ToString());

        //yield return new WaitForSeconds(0 * delayMult);

        hitBox.SetActive(true);
        hitBox.GetComponent<ItemHolder>().itemsHeld = dealer.GetComponent<ItemHolder>().itemsHeld;
        hitBox.GetComponent<meleeGeneral>().isCharged = isCharged;
        hitBox.transform.rotation = Quaternion.LookRotation(direction, new Vector3(1, 0, 0)) * Quaternion.Euler(0, 90, 180);
        //hitBox.transform.localScale *= scaleMult;
        hitBox.GetComponent<DealDamage>().owner = dealer;
        //hitBox.GetComponent<DealDamage>().finalDamageMult = dealer.GetComponent<DealDamage>().finalDamageMult;
        if (isPlayerTeam)
        {
            hitBox.layer = LayerMask.NameToLayer("HitEnemBulletsAndEnemies");
        }
        else
        {
            hitBox.layer = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
        }
        //Debug.Log("Attack damage mult: " + (damageMult * gameObject.GetComponent<DealDamage>().finalDamageMult).ToString());
        //hitBox.GetComponent<meleeGeneral>().isCharged = isThisAttackCharged;

        //if (objToIgnore != null)
        //{
        //    Physics2D.IgnoreCollision(hitBox.GetComponent<Collider2D>(), objToIgnore.GetComponent<Collider2D>(), true);
        //}

        //if (!allowSplit)
        //{
        //    Debug.Log("no splite1");
        //    hitBox.AddComponent<ItemSPLIT>();
        //    hitBox.GetComponent<ItemSPLIT>().canSplit = false;
        //}
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        if ((dealer.transform.position - target.transform.position).magnitude <= (range * 1.1f) + 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
