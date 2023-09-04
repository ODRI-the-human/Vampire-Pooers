using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSAWSHOT : ItemScript
{
    int timer = 0;
    public Vector3 bulletOffset = new Vector3(0, 0, 0);
    public GameObject guyLatchedTo;
    public bool dogma = false;
    public bool canDoTheThing = false;
    public bool isAProc = false;

    public GameObject SawShotVisual;

    // Start is called before the first frame update
    void Start()
    {
        DetermineShotRolls();
    }

    public void DetermineShotRolls()
    {
        canDoTheThing = true;
        int numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(20, gameObject, 900); // normally 20% chance
        if (numEffects > 0)
        {
            if (gameObject.GetComponent<checkAllLazerPositions>() == null && gameObject.GetComponent<meleeGeneral>() == null)
            {
                gameObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.saw;
                //gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
            }

            isAProc = true;
        }
    }


    public override void OnHit(GameObject enemo, GameObject shmenemo)
    {
        if (isAProc)
        {
            GameObject Poop = Instantiate(EntityReferencerGuy.Instance.sawVisual, new Vector3(-9999, 9999), Quaternion.Euler(0, 0, 0));
            Poop.tag = gameObject.tag;
            Poop.GetComponent<DealDamage>().overwriteDamageCalc = true;
            Poop.GetComponent<DealDamage>().finalDamageStat = gameObject.GetComponent<DealDamage>().GetDamageAmount() / 5;
            Poop.GetComponent<SawRotation>().instances = instances;

            if (gameObject.GetComponent<Bullet_Movement>() == null)
            {
                Poop.transform.localScale = 0.5f * transform.localScale;
            }
            else
            {
                Poop.transform.localScale = transform.localScale;
            }
                
            Poop.GetComponent<SawRotation>().guyLatchedTo = enemo;
            Poop.GetComponent<SawRotation>().advanceTimer = true;
            Poop.GetComponent<SawRotation>().bulletOffset = 0.5f * (transform.position - (Poop.GetComponent<SawRotation>().guyLatchedTo).transform.position).normalized;
            Poop.AddComponent<SawShotCreep>();

            GameObject owner = gameObject.GetComponent<DealDamage>().owner;
            Poop.GetComponent<DealDamage>().owner = owner;
            //if (gameObject.GetComponent<Bullet_Movement>() != null)
            //{
            //    owner.GetComponent<Attack>().bulletPool.Release(gameObject);
            //}
        }
    }
}
