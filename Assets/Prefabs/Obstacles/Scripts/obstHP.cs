using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstHP : MonoBehaviour
{
    public float HP;
    public float[] resistVals = new float[] { 99f, 99f, 0, 0, 0, 0, 0, 0 };

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        owMyEntireRockIsInPain(col.gameObject, col.gameObject.GetComponent<DealDamage>().GetDamageAmount());
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        owMyEntireRockIsInPain(col.gameObject, col.gameObject.GetComponent<DealDamage>().GetDamageAmount());
    }

    public void owMyEntireRockIsInPain(GameObject thingy, float damageAmt)
    {
        Debug.Log("bebeb rock damage momentz " + damageAmt);
        int damageType = thingy.GetComponent<DealDamage>().damageType;
        float resistVal = resistVals[damageType];

        HP -= damageAmt / resistVal;

        if (HP <= 0)
        {
            SendMessage("doOnDestroy");
            Destroy(gameObject);
        }
    }

    //void OnWallHit()
    //{

    //}

    public void doOnDestroy()
    {
        //shut up!
    }
}

