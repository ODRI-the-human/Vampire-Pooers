using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEP : MonoBehaviour
{
    int timerMarty = 0;
    public int instances = 1;
    public GameObject Creep;
    bool isPerson = false;

    void Start()
    {
        Creep = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().Creep;

        if (gameObject.tag == "Hostile")
        {
            isPerson = true;
            Creep = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().CreepHostile;
        }
        else
        {
            if (gameObject.tag == "Player")
            {
                isPerson = true;
                Creep = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().Creep;
            }
        }
    }

    void FixedUpdate()
    {
        if (timerMarty == 0 && isPerson)
        {
            GameObject newObject = Instantiate(Creep, transform.position + new Vector3(0, 0, 0), transform.rotation);
            newObject.transform.localScale *= 0.7f + 0.3f * instances;
            newObject.GetComponent<DealDamage>().overwriteDamageCalc = true;
            newObject.GetComponent<DealDamage>().damageAmt = 0.1f * gameObject.GetComponent<DealDamage>().finalDamageStat;
            newObject.GetComponent<DealDamage>().knockBackCoeff = 0;
            timerMarty = 20;
        }

        timerMarty--;
    }

    public void Undo()
    {
        //nothin
    }
}
