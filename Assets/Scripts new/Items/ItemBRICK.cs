using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBRICK : MonoBehaviour
{
    public int instances = 1;
    public bool isAProc;
    public Vector3 normieScale;

    void Start()
    {
        Debug.Log("brick added");

        normieScale = transform.localScale;

        if (gameObject.GetComponent<Bullet_Movement>() != null || gameObject.GetComponent<checkAllLazerPositions>() != null)
        {
            DetermineShotRolls();
        }
    }

    public void DetermineShotRolls()
    {
        float procMoment = 100f - 10 * gameObject.GetComponent<DealDamage>().procCoeff;
        float pringle = Random.Range(0f, 100f);
        isAProc = false;
        if (pringle > procMoment)
        {
            isAProc = true;
            gameObject.GetComponent<DealDamage>().finalDamageMult *= 4 * instances;
            if (gameObject.GetComponent<checkAllLazerPositions>() == null)
            {
                //transform.localScale = 2 * normieScale;

                if (gameObject.GetComponent<Bullet_Movement>() != null)
                {
                    gameObject.GetComponent<Bullet_Movement>().piercesLeft += 5000;
                }
            }
        }
    }

    void Update()
    {
        if (isAProc && gameObject.GetComponent<checkAllLazerPositions>() == null)
        {
            transform.localScale = 2 * normieScale;
        }
    }

    public void EndOfShotRolls()
    {
        if (isAProc)
        {
            Debug.Log("undid funny brick");
            gameObject.GetComponent<DealDamage>().finalDamageMult /= 4;
            transform.localScale = normieScale;
            gameObject.GetComponent<Bullet_Movement>().piercesLeft -= 5000;
        }
    }

    public void Undo()
    {
        Debug.Log("Removed brick");
        Destroy(this);
    }
}
