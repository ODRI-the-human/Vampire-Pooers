using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONTACT : MonoBehaviour
{
    public int instances = 1;
    GameObject Bingus; // This is the contact instance.

    void Start()
    {
        DetermineShotRolls();
    }

    // Start is called before the first frame update
    void DetermineShotRolls()
    {
        int LayerProjectileBlocking = LayerMask.NameToLayer("ProjectileBlocking");
        if ((gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet") && gameObject.layer != LayerProjectileBlocking && gameObject.GetComponent<faceInFunnyDirection>() == null) //the last one is to prevent it from being given to the baseball bat.
        {
            Bingus = Instantiate(EntityReferencerGuy.Instance.contactMan, transform.position, Quaternion.Euler(0, 0, 0));
            Bingus.GetComponent<dieOnContactWithBullet>().instances = 2 * instances;
            Bingus.transform.parent = gameObject.transform;
            Bingus.GetComponent<dieOnContactWithBullet>().master = gameObject;
            if (gameObject.GetComponent<darkArtMovement>() != null)
            {
                //Bingus.AddComponent<darkArtMovement>();
                Bingus.GetComponent<CapsuleCollider2D>().enabled = true;
                Bingus.GetComponent<CircleCollider2D>().enabled = false;
                //Bingus.GetComponent<darkArtMovement>().LorR = gameObject.GetComponent<darkArtMovement>().LorR;
                //Bingus.GetComponent<darkArtMovement>().initAngle = gameObject.GetComponent<darkArtMovement>().initAngle;
                //Bingus.GetComponent<darkArtMovement>().owner = gameObject.GetComponent<darkArtMovement>().owner;
                //Bingus.GetComponent<darkArtMovement>().timer = 0;
                Bingus.transform.SetParent(gameObject.transform);
                Bingus.transform.rotation = transform.rotation;
                Bingus.GetComponent<dieOnContactWithBullet>().instances = 50000000;
            }
            Bingus.tag = gameObject.tag;
            Bingus.transform.localScale = 2 * gameObject.transform.localScale;
        }
    }

    void EndOfShotRolls()
    {
        if (Bingus != null)
        {
            Destroy(Bingus);
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
