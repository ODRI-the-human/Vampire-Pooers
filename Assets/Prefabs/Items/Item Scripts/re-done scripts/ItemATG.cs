using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemATG : ItemScript
{
    public GameObject ATGMissile;
    public GameObject owner;
    float procMoment;
    float pringle;

    public override void OnHit(GameObject victim, GameObject source)
    {
        Component[] components = gameObject.GetComponents(typeof(Component));
        int scriptIndex = System.Array.IndexOf(components, this);

        int numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(10, source, scriptIndex); //10
        for (int i = 0; i < numEffects; i++)
        {
            GameObject ARSEMAN = Instantiate(ATGMissile, owner.transform.position, owner.transform.rotation);
            ARSEMAN.GetComponent<MissileTracking>().owner = owner;
            ARSEMAN.GetComponent<MissileTracking>().damageAmt = source.GetComponent<DealDamage>().damageToPassToVictim;
            ARSEMAN.GetComponent<MissileTracking>().instances = instances;
            ARSEMAN.GetComponent<MissileTracking>().scriptIndex = scriptIndex;

            if (gameObject.tag == "Hostile")
            {
                ARSEMAN.tag = "enemyBullet";
            }
            else
            {
                ARSEMAN.tag = "PlayerBullet";
            }
        }
    }

    void Start()
    {
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "Player")
        {
            ATGMissile = EntityReferencerGuy.Instance.ATGMissile;//MasterObject.GetComponent<EntityReferencerGuy>().ATGMissile;
        }
        else
        {
            ATGMissile = EntityReferencerGuy.Instance.ATGMissileHostile;
        }

        owner = gameObject.GetComponent<DealDamage>().owner;
    }
}
