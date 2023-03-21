using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponType : MonoBehaviour
{
    public int weaponHeld;
    public GameObject master;
    public GameObject spawnedBat;

    // Start is called before the first frame update
    void Awake()
    {

        if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
        {
            //weaponHeld = (int)ITEMLIST.PISTOL;
            master = GameObject.Find("bigFuckingMasterObject");
        }

        SetWeapon();
    }

    public void SetWeapon()
    {
        if (spawnedBat != null)
        {
            Destroy(spawnedBat);
        }

        switch (weaponHeld)
        {
            case (int)ITEMLIST.PISTOL:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().shotSpeed = 15;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.PISTOL;
                }
                break;
            case (int)ITEMLIST.GRENADELAUNCHER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.5f;
                    gameObject.GetComponent<Attack>().shotSpeed = 45;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.GRENADELAUNCHER;
                }
                break;
            case (int)ITEMLIST.DARKARTS:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 0.5f;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.DARKARTS;
                }
                break;
            case (int)ITEMLIST.LAZER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 5;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.2f;
                    gameObject.GetComponent<Attack>().shotSpeed = 10;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.LAZER;
                }
                break;
            case (int)ITEMLIST.BAT:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 6;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().holdDownToShoot = false;
                    spawnedBat = Instantiate(master.GetComponent<EntityReferencerGuy>().bat);
                    spawnedBat.GetComponent<faceInFunnyDirection>().owner = gameObject;
                    master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.BAT;

                }
                break;
        }
    }
}
