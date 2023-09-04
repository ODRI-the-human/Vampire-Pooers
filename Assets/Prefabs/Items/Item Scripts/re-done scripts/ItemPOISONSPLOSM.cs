using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPOISONSPLOSM : ItemScript
{
    public GameObject poisonSplosm;

    void Start()
    {
        //if (gameObject.tag == "Player")
        //{
        //    EventManager.DeathEffects += SpawnPoison;
        //    Debug.Log("Added poison wahoo");
        //}
        poisonSplosm = EntityReferencerGuy.Instance.poisonSplosm;
    }

    public override void OnKill(GameObject victim)
    {
        GameObject Marty = Instantiate(poisonSplosm, victim.transform.position, transform.rotation);
        Marty.transform.localScale *= (0.5f + instances * 0.5f);
        Marty.GetComponent<TriggerPoison>().owner = gameObject;
        Marty.GetComponent<TriggerPoison>().damageAmt = victim.GetComponent<HPDamageDie>().MaxHP / 40;
        if (victim.tag == "Hostile")
        {
            Marty.tag = "PlayerBullet";
        }
        else
        {
            Marty.tag = "enemyBullet";
        }
    }
}
