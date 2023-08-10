using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitIfKBVecHigh : MonoBehaviour
{
    public GameObject responsible;
    public GameObject ownerResponsible;
    public Vector3 lastSpeed;

    void Start()
    {
        ownerResponsible = responsible.GetComponent<DealDamage>().owner;
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude < 0.5f)
        {
            Invoke(nameof(RemoveScript), 0.1f); // Makes sure the damage has enough time to be dealt.
        }
        else
        {
            lastSpeed = gameObject.GetComponent<NewPlayerMovement>().knockBackVector;
        }
    }

    void RemoveScript()
    {
        Destroy(this);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //bool canColWithObj = true;
        //if (responsible != null)
        //{
        //    if (col.gameObject == responsible)
        //    {
        //        canColWithObj = false;
        //    }
        //}

        float damageAmt = 2 * gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude;
        if (col.gameObject.GetComponent<NewPlayerMovement>() != null)
        {
            col.gameObject.GetComponent<NewPlayerMovement>().knockBackVector = (col.gameObject.transform.position - transform.position).normalized * gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude;
            col.gameObject.AddComponent<hitIfKBVecHigh>();
            col.gameObject.GetComponent<hitIfKBVecHigh>().responsible = gameObject;
        }
        //col.gameObject.GetComponent<HPDamageDie>().Hurty(damageAmt, false, true);
        gameObject.GetComponent<HPDamageDie>().Hurty(damageAmt, false, 1, (int)DAMAGETYPES.NORMAL, false, null);
        gameObject.GetComponent<NewPlayerMovement>().knockBackVector = new Vector2(0, 0);
        Debug.Log("who shat myself, extent of penis: " + lastSpeed.magnitude);
    }
}
