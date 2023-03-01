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

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            LevelUp.levelEffects += goBerserk;
            Debug.Log("Added berserk wahoo");
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

    public void goBerserk()
    {
        if (gameObject.GetComponent<weaponType>().weaponHeld != (int)ITEMLIST.DARKARTS) //&& gameObject.GetComponent<LevelUp>().level % 2 == 0)
        {
            pastWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.DARKARTS;
            gameObject.GetComponent<Attack>().fireTimerLengthMLT /= 2;
            Debug.Log("Dingus");
            gameObject.GetComponent<weaponType>().SetWeapon();
            master.GetComponent<AudioSource>().volume = 0;
            spawnedMusic = Instantiate(music);
            spawnedRedPlane = Instantiate(redPlane);
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

        if (timer == 75 + 75 * instances && gameObject.GetComponent<weaponType>().weaponHeld == (int)ITEMLIST.DARKARTS)
        {
            gameObject.GetComponent<weaponType>().weaponHeld = pastWeapon;
            Debug.Log("Bringus");
            gameObject.GetComponent<weaponType>().SetWeapon();
            Destroy(spawnedMusic);
            Destroy(spawnedRedPlane);
            master.GetComponent<AudioSource>().volume = 1;
        }
    }

    public void Undo()
    {
        LevelUp.levelEffects -= goBerserk;
        EventManager.DeathEffects -= refreshBerserk;
    }
}
