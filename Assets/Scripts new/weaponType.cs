using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponType : MonoBehaviour
{
    public int weaponHeld;
    public GameObject master;
    public GameObject spawnedBat;


    //Stores the PREVIOUS stats of the weapon held, needed for when enemies use Dark Arts.
    public int previousFireType;
    public float previousFireTimerLengthMLT;

    void Start()
    {
        if (gameObject.GetComponent<Attack>() != null)
        {
            previousFireType = gameObject.GetComponent<Attack>().specialFireType;
            previousFireTimerLengthMLT = gameObject.GetComponent<Attack>().fireTimerLengthMLT;
        }

        if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
        {
            weaponHeld = (int)ITEMLIST.PISTOL;
            master = EntityReferencerGuy.Instance.master;
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
            case 0: // For resetting enemies' shit back to their normie settings.
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = previousFireType;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = previousFireTimerLengthMLT;
                }
                break;
            case (int)ITEMLIST.PISTOL:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().shotSpeed = 15;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                }
                break;
            case (int)ITEMLIST.GRENADELAUNCHER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.5f;
                    gameObject.GetComponent<Attack>().shotSpeed = 45;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                }
                if (gameObject.GetComponent<Attack>() == null)
                {
                    gameObject.AddComponent<explodeOnHit>();
                }
                break;
            case (int)ITEMLIST.DARKARTS:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    previousFireType = gameObject.GetComponent<Attack>().specialFireType;
                    previousFireTimerLengthMLT = gameObject.GetComponent<Attack>().fireTimerLengthMLT;

                    gameObject.GetComponent<Attack>().specialFireType = 3;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 0.5f;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                }
                break;
            case (int)ITEMLIST.LAZER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 5;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.2f;
                    gameObject.GetComponent<Attack>().shotSpeed = 10;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                }
                break;
            case (int)ITEMLIST.BAT:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 6;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().holdDownToShoot = false;
                    spawnedBat = Instantiate(EntityReferencerGuy.Instance.bat);
                    spawnedBat.GetComponent<faceInFunnyDirection>().owner = gameObject;
                }
                break;
        }
    }
}
