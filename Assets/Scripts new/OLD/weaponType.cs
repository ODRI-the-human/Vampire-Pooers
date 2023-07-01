using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This was deprecated and moved to ItemHolder, because ya know it's just simpler!


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

        //if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
        //{
        //    weaponHeld = (int)ITEMLIST.PISTOL;
        master = EntityReferencerGuy.Instance.master;
        //}

        //SetWeapon(weaponHeld);
    }

    public void SetWeapon(int weaponToGive)
    {
        if (gameObject.GetComponent<BatVisual>() != null)
        {
            gameObject.GetComponent<BatVisual>().Kill();
        }

        Debug.Log("weapon set xd");        

        switch (weaponToGive)
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
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.regularShotParticle;
                }
                break;
            case (int)ITEMLIST.GRENADELAUNCHER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.5f;
                    gameObject.GetComponent<Attack>().shotSpeed = 45;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.regularShotParticle;
                }
                if (gameObject.GetComponent<Attack>() == null)
                {
                    gameObject.GetComponent<DealDamage>().damageBase *= 1.5f;
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
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                }
                break;
            case (int)ITEMLIST.LAZER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 5;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.2f;
                    gameObject.GetComponent<Attack>().shotSpeed = 10;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                }
                break;
            case (int)ITEMLIST.BAT:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    //gameObject.GetComponent<NewPlayerMovement>().AttackStatus(false); // otherwise the player is slow (silly)
                    //gameObject.GetComponent<Attack>().specialFireType = 6;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().holdDownToShoot = false;
                    //spawnedBat = Instantiate(EntityReferencerGuy.Instance.bat);
                    //spawnedBat.GetComponent<faceInFunnyDirection>().owner = gameObject;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                    gameObject.AddComponent<BatVisual>();

                    gameObject.GetComponent<Attack>().meleeHitObj = EntityReferencerGuy.Instance.batHitbox;
                    gameObject.GetComponent<Attack>().hitboxSpawnDelay = 0.1f;
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                }
                break;
        }

        weaponHeld = weaponToGive;
    }
}
