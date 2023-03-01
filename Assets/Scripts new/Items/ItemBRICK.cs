using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBRICK : MonoBehaviour
{
    public int instances = 1;

    // Start is called before the first frame update
    void Start()
    {
        if ((gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet") && (gameObject.GetComponent<DealDamage>().isBulletClone || gameObject.GetComponent<checkAllLazerPositions>() != null))
        {
            float procMoment = 100f - instances * 10 * gameObject.GetComponent<DealDamage>().procCoeff;
            float pringle = Random.Range(0f, 100f);
            if (pringle > procMoment)
            {
                gameObject.GetComponent<DealDamage>().finalDamageMult *= 4;
                if (gameObject.GetComponent<checkAllLazerPositions>() == null)
                {
                    gameObject.AddComponent<ItemPIERCING>();
                    gameObject.AddComponent<ItemPIERCING>().giveDamageBonus = false;
                    gameObject.GetComponent<ItemPIERCING>().instances = 5000;
                    gameObject.transform.localScale *= 2;
                }
            }
        }
    }

    public void Undo()
    {
        //nothin
    }
}
