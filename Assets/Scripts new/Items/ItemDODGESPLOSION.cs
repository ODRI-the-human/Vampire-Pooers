using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDODGESPLOSION : MonoBehaviour
{

    GameObject dodgeSplosion;
    GameObject contacter;
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dodgeSplosion = EntityReferencerGuy.Instance.dodgeSplosion;
        contacter = EntityReferencerGuy.Instance.contactMan;
    }

    public void DodgeEndEffects()
    {
        GameObject explodyDodge = Instantiate(dodgeSplosion, transform.position, transform.rotation);
        explodyDodge.transform.localScale *= 2.5f + 1.2f * instances;
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
