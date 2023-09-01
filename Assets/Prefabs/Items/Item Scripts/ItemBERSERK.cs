using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBERSERK : MonoBehaviour
{
    public int timer = 9999;
    int pastWeapon;

    GameObject master;
    GameObject redPlane;

    public int instances = 1;
    bool isActive = false;
    AbilityParams lastPrimary;

    void LevelEffects()
    {
        if (gameObject.GetComponent<LevelUp>().level % 2 == 0)
        {
            if (!isActive)
            {
                isActive = true;
                lastPrimary = gameObject.GetComponent<Attack>().abilityTypes[0];
                gameObject.GetComponent<Attack>().abilityTypes[0] = EntityReferencerGuy.Instance.berserkAttack;
                redPlane = Instantiate(EntityReferencerGuy.Instance.berserkPlane);
                timer = 100 * (instances + 1);
            }
            else
            {
                timer = 100 * (instances + 1);
            }
        }
    }

    public void ApplyItemOnDeaths(GameObject who)
    {
        if (isActive)
        {
            timer = Mathf.Clamp(timer + 50, 0, 100 * (instances + 1));
        }
    }

    void EndAbility()
    {
        gameObject.GetComponent<Attack>().abilityTypes[0] = lastPrimary;
        Destroy(redPlane);
    }

    void FixedUpdate()
    {
        if (isActive && gameObject.GetComponent<Attack>().abilityTypes[0] != EntityReferencerGuy.Instance.berserkAttack)
        {
            lastPrimary = gameObject.GetComponent<Attack>().abilityTypes[0];
            gameObject.GetComponent<Attack>().abilityTypes[0] = EntityReferencerGuy.Instance.berserkAttack;
        }

        timer--;
        if (timer <= 0 && isActive)
        {
            EndAbility();
        }
    }

    //void IncreaseInstances(string name)
    //{
    //    if (name == this.GetType().ToString())
    //    {
    //        instances++;
    //    }
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Debug.Log("Added berserk wahoo");
    //    if (gameObject.tag == "Player")
    //    {
    //        //Debug.Log("Added berserk wahoo");
    //    }
    //    master = EntityReferencerGuy.Instance.master;
    //    music = EntityReferencerGuy.Instance.berserkMusic;
    //    redPlane = EntityReferencerGuy.Instance.berserkPlane;
    //}

    //public void ApplyItemOnDeaths(GameObject who)
    //{
    //    if (gameObject.GetComponent<weaponType>().weaponHeld == (int)ITEMLIST.DARKARTS)
    //    {
    //        timer -= 50;
    //    }
    //}

    //public void LevelEffects()
    //{
    //    if (gameObject.GetComponent<ItemHolder>().weaponHeld != (int)ITEMLIST.DARKARTS && gameObject.GetComponent<LevelUp>().level % 2 == 0)
    //    {
    //        pastWeapon = gameObject.GetComponent<ItemHolder>().weaponHeld;
    //        Debug.Log("Dingus");
    //        gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.DARKARTS);
    //        if (gameObject.tag == "Player")
    //        {
    //            master.GetComponent<AudioSource>().volume = 0;
    //            spawnedMusic = Instantiate(music);
    //            spawnedRedPlane = Instantiate(redPlane);
    //        }
    //        isActive = true;
    //    }
    //    timer = 0;
    //}

    //void FixedUpdate()
    //{
    //    timer++;
    //    if (timer < 0)
    //    {
    //        timer = 0;
    //    }

    //    if (isActive && gameObject.GetComponent<ItemHolder>().weaponHeld != (int)ITEMLIST.DARKARTS) // For if the player picks up a new weapon while this is active.
    //    {
    //        pastWeapon = gameObject.GetComponent<ItemHolder>().weaponHeld;
    //        gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.DARKARTS);
    //    }

    //    if (timer == 75 + 75 * instances && gameObject.GetComponent<ItemHolder>().weaponHeld == (int)ITEMLIST.DARKARTS)
    //    {
    //        EndBerserk();
    //    }
    //}

    //public void EndBerserk()
    //{
    //    Debug.Log("Bringus");
    //    gameObject.GetComponent<ItemHolder>().SetWeapon(pastWeapon);
    //    gameObject.GetComponent<Attack>().bulletPool.Dispose();
    //    if (gameObject.tag == "Player")
    //    {
    //        Destroy(spawnedMusic);
    //        Destroy(spawnedRedPlane);
    //        master.GetComponent<AudioSource>().volume = 1;
    //    }
    //    isActive = false;
    //}

    //public void Undo()
    //{
    //    Destroy(this);
    //}
}
