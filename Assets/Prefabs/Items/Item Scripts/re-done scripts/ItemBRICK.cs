using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBRICK : ItemScript
{
    public bool isAProc;
    public Vector3 normieScale;

    public override float DamageMult()
    {
        float damageMult = 1;

        if (isAProc)
        {
            damageMult = instances * 4f;
        }

        return damageMult;
    }

    void Start()
    {
        Debug.Log("brick added");

        float procMoment = 100f - 10 * gameObject.GetComponent<DealDamage>().procCoeff;
        float pringle = Random.Range(0f, 100f);
        isAProc = false;
        if (pringle > procMoment)
        {
            isAProc = true;
            if (gameObject.GetComponent<checkAllLazerPositions>() == null)
            {
                transform.localScale *= 2f;
                gameObject.GetComponent<DealDamage>().massCoeff *= 4f;

                if (gameObject.GetComponent<Bullet_Movement>() != null)
                {
                    gameObject.GetComponent<Bullet_Movement>().piercesLeft += 5000;
                }
            }
        }
    }
}
