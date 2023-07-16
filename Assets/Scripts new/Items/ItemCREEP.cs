using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEP : MonoBehaviour
{
    int timerMarty = 0;
    public int instances = 1;
    public GameObject Creep;
    bool isPerson = false;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        if (gameObject.tag == "Hostile")
        {
            isPerson = true;
            Creep = EntityReferencerGuy.Instance.CreepHostile;
        }
        else
        {
            if (gameObject.tag == "Player")
            {
                isPerson = true;
                Creep = EntityReferencerGuy.Instance.Creep;
            }
        }
    }

    void FixedUpdate()
    {
        if (timerMarty == 0 && isPerson)
        {
            GameObject newObject = Instantiate(Creep, transform.position + new Vector3(0, 0, 0), transform.rotation);
            newObject.transform.localScale *= 0.3f + 0.2f * instances;
            newObject.GetComponent<DealDamage>().overwriteDamageCalc = true;
            newObject.GetComponent<DealDamage>().finalDamageStat = 0.1f * gameObject.GetComponent<DealDamage>().GetDamageAmount() * instances;
            Debug.Log((0.1f * gameObject.GetComponent<DealDamage>().GetDamageAmount() * instances).ToString());
            newObject.GetComponent<DealDamage>().massCoeff = 0;
            timerMarty = 20;
        }

        timerMarty--;
    }

    public void Undo()
    {
        Destroy(this);
    }
}
