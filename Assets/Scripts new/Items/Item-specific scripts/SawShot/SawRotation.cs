using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotation : MonoBehaviour
{
    public int mode = 0;
    public int timer = 0;
    public int instances = 0;
    public bool advanceTimer = false;

    public GameObject guyLatchedTo;
    public Vector3 bulletOffset;

    void FixedUpdate()
    {
        if (timer % 10 == 0)
        {
            float procMoment = 100f - 100f * gameObject.GetComponent<DealDamage>().critProb * gameObject.GetComponent<DealDamage>().procCoeff;
            float pringle = Random.Range(0f, 100f);
            float critMult = 1;
            bool isCrit = false;
            if (pringle > procMoment)
            {
                critMult = gameObject.GetComponent<DealDamage>().critMult;
                //Instantiate(CritAudio);
                isCrit = true;
            }
            float damageAmount = gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;

            guyLatchedTo.GetComponent<HPDamageDie>().Hurty(damageAmount, isCrit, true, 1, (int)DAMAGETYPES.NORMAL, true);
        }

        if (advanceTimer)
        {
            timer++;
        }
    }

    void Update()
    {
        if (guyLatchedTo == null || timer == 100 * instances)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = guyLatchedTo.transform.position + bulletOffset;
        }

        transform.rotation *= Quaternion.Euler(0, 0, 240 * Time.deltaTime);
    }
}
