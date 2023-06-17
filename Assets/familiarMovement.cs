using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familiarMovement : MonoBehaviour
{
    public int gunnerType; // Stores what gunner this is, i.e. homing/normie/auto etc.
    public GameObject toFollow;
    public GameObject owner;

    void Start()
    {
        gameObject.GetComponent<Attack>().massToGiveBullets = 0.333f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunnerType != (int)ITEMLIST.AUTOFAMILIAR)
        {
            Vector3 vec3 = Vector3.zero;

            if (owner.tag == "Player")
            {
                vec3 = owner.GetComponent<Attack>().reticle.transform.position - transform.position;
                gameObject.GetComponent<Attack>().isFiring = owner.GetComponent<Attack>().isHoldingFire;
            }
            else
            {
                vec3 = owner.GetComponent<Attack>().currentTarget.transform.position - transform.position;
                gameObject.GetComponent<Attack>().isFiring = true;
                gameObject.GetComponent<Attack>().shotSpeed = 4;
            }
            gameObject.GetComponent<Attack>().vectorToTarget = new Vector2(vec3.x, vec3.y).normalized;
            //gameObject.GetComponent<Attack>().vectorToTarget = owner.GetComponent<Attack>().vectorToTarget;
        }
       
        if (toFollow == null)
        {
            Destroy(gameObject);
        }

        transform.position += 2 * Time.deltaTime * (toFollow.transform.position - gameObject.transform.position);
    }
}
