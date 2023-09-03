using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemELECTRIC : ItemScript
{
    public GameObject masterObject;
    public List<GameObject> enemiesEffected = new List<GameObject>();
    public GameObject owner;
    GameObject GUY;

    bool dealTheDamage = true;

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            owner = gameObject;
        }
        else
        {
            owner = gameObject.GetComponent<DealDamage>().owner;
        }
    }

    public override void OnHit(GameObject victim, GameObject responsible)
    {
        GameObject gamer = victim;

        if (gamer.tag != "Wall")
        {
            if (!enemiesEffected.Contains(gamer))
            {
                enemiesEffected.Add(gamer);
                gamer.GetComponent<Statuses>().AddStatus((int)STATUSES.ELECTRIC, 0, gameObject);
            }
        }

        masterObject = EntityReferencerGuy.Instance.master;
        int j = 0;

        foreach (GameObject Gareth in enemiesEffected)
        {
            if (Gareth != null && Gareth != gamer)
            {
                //Gareth.GetComponent<HPDamageDie>().HP -= 10 * instances;
                //Gareth.GetComponent<HPDamageDie>().sprite.color = Color.red;
                Gareth.GetComponent<HPDamageDie>().Hurty(10 * instances, false, 0, (int)DAMAGETYPES.ELECTRIC, true, null, false);
                //Gareth.GetComponent<HPDamageDie>().colorChangeTimer = 1;
                //masterObject.GetComponent<showDamageNumbers>().showDamage(Gareth.transform.position, 10 * instances, (int)DAMAGETYPES.ELECTRIC, false);
            }
            if (Gareth == null)
            {
                enemiesEffected.RemoveAt(j);
            }

            j++;
        }

        //for (int i = gosToRemove.Count - 1; i > 0; i--)
        //{
        //    Debug.Log("gass");
        //    enemiesEffected.RemoveAt(gosToRemove[i]);
        //}
    }
}
