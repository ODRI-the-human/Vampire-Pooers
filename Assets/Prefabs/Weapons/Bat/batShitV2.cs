using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batShitV2 : MonoBehaviour
{
    GameObject camera;
    GameObject master;
    public GameObject hitSFX;

    public bool isChargedHit = false;

    void Start()
    {
        camera = EntityReferencerGuy.Instance.camera;
        master = EntityReferencerGuy.Instance.master;
        isChargedHit = gameObject.GetComponent<meleeGeneral>().isCharged;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hostile")
        {
            if (isChargedHit)
            {
                Debug.Log("hey that attack was charged up, good job buddy!");
                col.gameObject.AddComponent<hitIfKBVecHigh>();
                Vector3 hitVec = col.gameObject.transform.position - gameObject.GetComponent<DealDamage>().owner.transform.position;
                gameObject.GetComponent<DealDamage>().massCoeff = 0;
                col.gameObject.GetComponent<NewPlayerMovement>().knockBackVector = 55 * new Vector2(hitVec.x, hitVec.y).normalized;
                gameObject.GetComponent<DealDamage>().massCoeff = 2;
                master.GetComponent<visualPoopoo>().bigHitFreeze(0.02f);
            }

            if (col.gameObject.GetComponent<HPDamageDie>().iFrames <= 0)
            {
                Instantiate(hitSFX);
            }
        }

        if (col.gameObject.tag != gameObject.tag && col.gameObject.GetComponent<Bullet_Movement>() != null && isChargedHit)
        {
            if (gameObject.GetComponent<dieOnContactWithBullet>() == null)
            {
                gameObject.AddComponent<dieOnContactWithBullet>();
                gameObject.GetComponent<dieOnContactWithBullet>().instances = 0;
                gameObject.GetComponent<dieOnContactWithBullet>().reduceInstOnHit = false;
                gameObject.GetComponent<dieOnContactWithBullet>().master = gameObject;
            }
            col.gameObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            col.gameObject.GetComponent<ItemHolder>().ApplyItems();
            col.gameObject.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
            if (col.gameObject.GetComponent<Bullet_Movement>() != null)
            {
                col.gameObject.GetComponent<Bullet_Movement>().speed = 30;
            }
        }
    }
}
