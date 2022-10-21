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

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
    }

    public void Sprinkle(int which)
    {
        Invoke(nameof(DeleteItems), 0);
        poohead = which;
    }

    public void DeleteItems()
    {
        int counterGuy = 0;
        switch (poohead)
        {
            case 0: // removes all items.
                scripts = GetComponents(typeof(MonoBehaviour));
                foreach (Component thing in scripts)
                {
                    counterGuy++;
                    if (counterGuy > numNormalScripts && counterGuy != scripts.Length) // Only does the following for the 13th script onwards, and doesn't do it for the last script.
                    {
                        Destroy(thing);
                    }
                }
                foreach (GameObject familiar in familiars)
                {
                    Destroy(familiar);
                }
                familiars.Clear();
                break;
            case 1: // removes first item.
                break;
        }
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
            newGuy.GetComponent<Attack>().Bullet = master.GetComponent<EntityReferencerGuy>().enemyBullet;
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
            case (int)ITEMLIST. AUTOFAMILIAR:
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
}
