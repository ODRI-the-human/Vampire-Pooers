using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitIfKBVecHigh : MonoBehaviour
{
    public GameObject responsible;

    void FixedUpdate()
    {
        if (gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude < 0.1f)
        {
            Invoke(nameof(RemoveScript), 0.02f); // Makes sure the damage has enough time to be dealt.
        }
    }

    void RemoveScript()
    {
        Destroy(this);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hostile" && col.gameObject != responsible)
        {
            float damageAmt = 3 * gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude;
            col.gameObject.GetComponent<NewPlayerMovement>().knockBackVector = (col.gameObject.transform.position - transform.position).normalized * gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude;
            col.gameObject.AddComponent<hitIfKBVecHigh>();
            col.gameObject.GetComponent<hitIfKBVecHigh>().responsible = gameObject;
            //col.gameObject.GetComponent<HPDamageDie>().Hurty(damageAmt, false, true);
            gameObject.GetComponent<HPDamageDie>().Hurty(damageAmt, false, 1, (int)DAMAGETYPES.NORMAL, false, gameObject);
            gameObject.GetComponent<NewPlayerMovement>().knockBackVector = new Vector2(0, 0);
        }

        if (col.gameObject.tag == "Wall")
        {
            float damageAmt = 2.5f * gameObject.GetComponent<NewPlayerMovement>().knockBackVector.magnitude;
            gameObject.GetComponent<HPDamageDie>().Hurty(0.5f * damageAmt, false, 1, (int)DAMAGETYPES.NORMAL, false, gameObject);
            gameObject.GetComponent<NewPlayerMovement>().knockBackVector = new Vector2(0, 0);
        }
    }
}
