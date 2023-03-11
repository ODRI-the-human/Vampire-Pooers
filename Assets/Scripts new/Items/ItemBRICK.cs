using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBRICK : MonoBehaviour
{
    public int instances = 1;
    public bool isAProc;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("brick added");

        if ((gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet") && (gameObject.GetComponent<DealDamage>().isBulletClone || gameObject.GetComponent<checkAllLazerPositions>() != null))
        {
            DetermineShotRolls();
        }
    }

    public void DetermineShotRolls()
    {
        float procMoment = 100f - instances * 10 * gameObject.GetComponent<DealDamage>().procCoeff;
        float pringle = Random.Range(0f, 100f);
        isAProc = false;
        if (pringle > procMoment)
        {
            isAProc = true;
            gameObject.GetComponent<DealDamage>().finalDamageMult *= 4;
            if (gameObject.GetComponent<checkAllLazerPositions>() == null)
            {
                gameObject.transform.localScale *= 2;

                if (gameObject.GetComponent<Bullet_Movement>() != null)
                {
                    gameObject.GetComponent<Bullet_Movement>().piercesLeft = 5000;
                }
            }
        }
    }

    public void EndOfShotRolls()
    {
        if (isAProc)
        {
            Debug.Log("undid funny brick");
            gameObject.GetComponent<DealDamage>().finalDamageMult /= 4;
            gameObject.transform.localScale /= 2;
        }
    }

    public void Undo()
    {
        Debug.Log("Removed brick");
        Destroy(this);
    }
}
