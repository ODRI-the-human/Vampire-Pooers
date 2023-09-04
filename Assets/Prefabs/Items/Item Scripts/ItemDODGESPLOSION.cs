using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDODGESPLOSION : ItemScript
{

    GameObject dodgeSplosion;
    GameObject contacter;

    // Start is called before the first frame update
    void Start()
    {
        dodgeSplosion = EntityReferencerGuy.Instance.dodgeSplosion;
        contacter = EntityReferencerGuy.Instance.contactMan;
    }

    public override void OnDodgeEnd()
    {
        GameObject explodyDodge = Instantiate(dodgeSplosion, transform.position, transform.rotation);
        explodyDodge.GetComponent<DealDamage>().finalDamageStat = 2 * gameObject.GetComponent<DealDamage>().GetDamageAmount();
        explodyDodge.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
        explodyDodge.transform.localScale *= 2.5f + 1.25f * instances;
        if (gameObject.tag == "Hostile")
        {
            explodyDodge.tag = "enemyBullet";
        }
        GameObject explodySodge = Instantiate(contacter, transform.position, transform.rotation);
        explodySodge.transform.localScale = explodyDodge.transform.localScale / 6;
        explodySodge.GetComponent<dieOnContactWithBullet>().master = explodyDodge;
        explodySodge.GetComponent<dieOnContactWithBullet>().calcScaleAuto = false;
    }

    public void Undo()
    {
        Destroy(this);
    }
}
