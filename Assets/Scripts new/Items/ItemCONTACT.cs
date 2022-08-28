using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONTACT : MonoBehaviour
{
    public int instances = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
        {
            GameObject Bingus = Instantiate(GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().contactMan);
            Bingus.GetComponent<dieOnContactWithBullet>().instances = instances;
            if (gameObject.GetComponent<darkArtMovement>() != null)
            {
                Bingus.AddComponent<darkArtMovement>();
                Bingus.GetComponent<CapsuleCollider2D>().enabled = true;
                Bingus.GetComponent<CircleCollider2D>().enabled = false;
                Bingus.GetComponent<darkArtMovement>().LorR = gameObject.GetComponent<darkArtMovement>().LorR;
                Bingus.GetComponent<darkArtMovement>().initAngle = gameObject.GetComponent<darkArtMovement>().initAngle;
                Bingus.GetComponent<darkArtMovement>().owner = gameObject.GetComponent<darkArtMovement>().owner;
                Bingus.GetComponent<darkArtMovement>().timer = 0;
                Bingus.GetComponent<dieOnContactWithBullet>().instances *= 4;
            }
            Bingus.tag = gameObject.tag;
            Bingus.GetComponent<dieOnContactWithBullet>().master = gameObject;
        }
    }
}
