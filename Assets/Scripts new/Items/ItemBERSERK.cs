using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBERSERK : MonoBehaviour
{
    public int timer = 9999;
    int pastWeapon;

    GameObject master;
    GameObject music;
    GameObject spawnedMusic;
    GameObject redPlane;
    GameObject spawnedRedPlane;

    public int instances = 1;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Added berserk wahoo");
        if (gameObject.tag == "Player")
        {
            //Debug.Log("Added berserk wahoo");
            EventManager.DeathEffects += refreshBerserk;
        }
        master = GameObject.Find("bigFuckingMasterObject");
        music = master.GetComponent<EntityReferencerGuy>().berserkMusic;
        redPlane = master.GetComponent<EntityReferencerGuy>().berserkPlane;
    }

    public void refreshBerserk(Vector3 pos)
    {
        if (gameObject.GetComponent<weaponType>().weaponHeld == (int)ITEMLIST.DARKARTS)
        {
            timer -= 50;
        }
    }

    public void LevelEffects()
    {
        if (gameObject.GetComponent<weaponType>().weaponHeld != (int)ITEMLIST.DARKARTS && gameObject.GetComponent<LevelUp>().level % 2 == 0)
        {
            pastWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.DARKARTS;
            Debug.Log("Dingus");
            gameObject.GetComponent<weaponType>().SetWeapon();
            if (gameObject.tag == "Player")
            {
                master.GetComponent<AudioSource>().volume = 0;
                spawnedMusic = Instantiate(music);
                spawnedRedPlane = Instantiate(redPlane);
            }
            isActive = true;
        }
        timer = 0;
    }

    void FixedUpdate()
    {
        timer++;
        if (timer < 0)
        {
            timer = 0;
        }

        if (isActive && gameObject.GetComponent<weaponType>().weaponHeld != (int)ITEMLIST.DARKARTS) // For if the player picks up a new weapon while this is active.
        {
            pastWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.DARKARTS;
            gameObject.GetComponent<weaponType>().SetWeapon();
        }

        if (timer == 75 + 75 * instances && gameObject.GetComponent<weaponType>().weaponHeld == (int)ITEMLIST.DARKARTS)
        {
            EndBerserk();
        }
    }

    public void EndBerserk()
    {
        gameObject.GetComponent<weaponType>().weaponHeld = pastWeapon;
        Debug.Log("Bringus");
        gameObject.GetComponent<weaponType>().SetWeapon();
        if (gameObject.tag == "Player")
        {
            Destroy(spawnedMusic);
            Destroy(spawnedRedPlane);
            master.GetComponent<AudioSource>().volume = 1;
        }
        isActive = false;
    }

    public void Undo()
    {
        EventManager.DeathEffects -= refreshBerserk;
        Destroy(this);
    }
}
