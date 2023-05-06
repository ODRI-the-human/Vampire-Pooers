using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherStuff : MonoBehaviour
{
    public Component[] scripts;
    int numNormalScripts = 12;
    int poohead;
    public List<GameObject> familiars = new List<GameObject>();
    GameObject master;

    public int familiarBonusDMG = 0;
    public int familiarBonusFIRERATE = 0;
    public int familiarBonusHOMING = 0;

    public bool canStillHeal = true;
    public List<int> itemsToGiveRoundly = new List<int>();
    public GameObject itemSelector;

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    public void Undo() // clears familiars for the reroll item.
    {
        foreach (GameObject familiar in familiars)
        {
            Destroy(familiar);
        }
        familiars.Clear();
        familiarBonusDMG = 0;
        familiarBonusFIRERATE = 0;
        familiarBonusHOMING = 0;
    }

    public void AddNewFamiliar(GameObject newGuy, int item)
    {
        if (familiars.Count > 0)
        {
            newGuy.GetComponent<familiarMovement>().toFollow = familiars[familiars.Count - 1];
        }
        else
        {
            newGuy.GetComponent<familiarMovement>().toFollow = gameObject;
        }
        familiars.Add(newGuy);
        if (gameObject.tag == "Hostile")
        {
            newGuy.tag = "enemyFamiliar";
            newGuy.GetComponent<Attack>().Bullet = EntityReferencerGuy.Instance.enemyBullet;
            newGuy.GetComponent<Attack>().playerControlled = false;
        }
        switch (item)
        {
            case (int)ITEMLIST.FAMILIAR:
                familiarBonusDMG += 1;
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                familiarBonusHOMING += 1;
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                familiarBonusFIRERATE += 1;
                break;
        }

        SetAllStats();
    }

    public void SetAllStats()
    {
        foreach (GameObject familiar in familiars)
        {
            familiar.GetComponent<ItemHolder>().itemsHeld.Clear();
            for (int i = 0; i < familiarBonusHOMING; i++)
            {
                familiar.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.HOMING);
            }
            familiar.GetComponent<DealDamage>().finalDamageMult = 1 + 0.5f * familiarBonusDMG;
            familiar.GetComponent<Attack>().fireTimerDIV = 1 + 0.5f * familiarBonusFIRERATE;
        }
    }

    //void Update()
    //{
    //    // For making it possible to click on items to pick them up.
    //    if (Input.GetButtonDown("Fire1") && gameObject.tag == "Player")
    //    {
    //        Vector2 spawnPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    //        GameObject BumboSoccer = Instantiate(itemSelector, spawnPos, transform.rotation);
    //        BumboSoccer.GetComponent<mouseItemSelection>().master = gameObject;
    //    }
    //}
}
