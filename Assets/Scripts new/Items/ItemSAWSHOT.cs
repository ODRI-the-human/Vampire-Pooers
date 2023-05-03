using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSAWSHOT : MonoBehaviour
{
    public int instances = 1;
    int timer = 0;
    public Vector3 bulletOffset = new Vector3(0, 0, 0);
    public GameObject guyLatchedTo;
    public bool dogma = false;
    public bool canDoTheThing = false;
    public bool isAProc = false;

    public GameObject SawShotVisual;

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
        if (gameObject.GetComponent<Bullet_Movement>() != null || gameObject.GetComponent<checkAllLazerPositions>() != null)
        {
            DetermineShotRolls();
        }
    }

    public void DetermineShotRolls()
    {
        canDoTheThing = true;
        int numEffects = gameObject.GetComponent<DealDamage>().ChanceRoll(20, gameObject, 900);
        if (numEffects == 1)
        {
            if (gameObject.GetComponent<checkAllLazerPositions>() == null && gameObject.GetComponent<isMelee>() == null)
            {
                gameObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.saw;
                gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
            }

            isAProc = true;
        }
    }

    public void EndOfShotRolls()
    {
        if (isAProc && (gameObject.GetComponent<checkAllLazerPositions>() == null && gameObject.GetComponent<isMelee>() == null))
        {
            gameObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.bullet;
            gameObject.GetComponent<Bullet_Movement>().piercesLeft -= 1;
        }

        isAProc = false;
    }


    void RollOnHit(GameObject[] objects)
    {
        GameObject enemo = objects[0];

        if (isAProc)
        {
            GameObject Poop = Instantiate(EntityReferencerGuy.Instance.sawVisual, new Vector3(-9999, 9999), Quaternion.Euler(0, 0, 0));
            Poop.tag = gameObject.tag;
            Poop.GetComponent<DealDamage>().overwriteDamageCalc = true;
            Poop.GetComponent<DealDamage>().damageAmt = gameObject.GetComponent<DealDamage>().finalDamageStat / 5;
            Poop.GetComponent<SawRotation>().instances = instances;

            if (gameObject.GetComponent<checkAllLazerPositions>() != null)
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
            //Poop.AddComponent<SawShotCreep>();

            GameObject owner = gameObject.GetComponent<DealDamage>().owner;
            Poop.GetComponent<DealDamage>().owner = owner;
            if (gameObject.GetComponent<Bullet_Movement>() != null)
            {
                owner.GetComponent<Attack>().bulletPool.Release(gameObject);
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    RollOnHits(col.gameObject);
    //}

    void Undo()
    {
        Destroy(this);
    }
}
