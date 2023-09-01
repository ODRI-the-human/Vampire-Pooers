using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemATG : ItemScript
{
    public GameObject ATGMissile;
    public GameObject owner;
    float procMoment;
    float pringle;

    public override void AddInstance()
    {
        Debug.Log("instances of atg increased, pog1!");
        instances++;
    }

    public override void RemoveInstance()
    {
        instances--;
        if (instances == 0)
        {
            Destroy(this);
        }
    }

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

    public override void OnKill()
    {

    }

    public override void OnHurt()
    {

    }

    public override void OnLevel()
    {

    }

    public override float DamageMult()
    {
        return 1f;
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
