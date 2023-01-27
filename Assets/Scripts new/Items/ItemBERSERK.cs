using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBERSERK : MonoBehaviour
{
    public int timer = 9999;
    int pastWeapon;

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
        music = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().berserkMusic;
        redPlane = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().berserkPlane;
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
        if (gameObject.GetComponent<weaponType>().weaponHeld != (int)ITEMLIST.DARKARTS && gameObject.GetComponent<LevelUp>().level % 2 == 0)
        {
            pastWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.DARKARTS;
            gameObject.GetComponent<Attack>().fireTimerLengthMLT /= 2;
            Debug.Log("Dingus");
            gameObject.GetComponent<weaponType>().SetWeapon();
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

        //if (timer == 150 * instances && gameObject.GetComponent<weaponType>().weaponHeld == (int)WEAPONS.DARKARTS)
        //{
        //    gameObject.GetComponent<weaponType>().weaponHeld = pastWeapon;
        //    Debug.Log("Bringus");
        //    gameObject.GetComponent<weaponType>().SetWeapon();
        //    gameObject.GetComponent<Attack>().fireTimerLengthMLT *= 2;
        //    Destroy(spawnedMusic);
        //    Destroy(spawnedRedPlane);
        //}
    }

    public void Undo()
    {
        LevelUp.levelEffects -= goBerserk;
        EventManager.DeathEffects -= refreshBerserk;
    }
}
