using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPOISONSPLOSM : MonoBehaviour
{
    public int instances = 1;
    public GameObject poisonSplosm;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        //if (gameObject.tag == "Player")
        //{
        //    EventManager.DeathEffects += SpawnPoison;
        //    Debug.Log("Added poison wahoo");
        //}
        poisonSplosm = EntityReferencerGuy.Instance.poisonSplosm;
    }

    public void ApplyItemOnDeaths(GameObject who)
    {
        GameObject Marty = Instantiate(poisonSplosm, who.transform.position, transform.rotation);
        Marty.transform.localScale *= (0.5f + instances * 0.5f);
        Marty.GetComponent<TriggerPoison>().owner = gameObject;
        Marty.GetComponent<TriggerPoison>().damageAmt = who.GetComponent<HPDamageDie>().MaxHP / 40;
        if (who.tag == "Hostile")
        {
            Marty.tag = "PlayerBullet";
        }
        else
        {
            Marty.tag = "enemyBullet";
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
