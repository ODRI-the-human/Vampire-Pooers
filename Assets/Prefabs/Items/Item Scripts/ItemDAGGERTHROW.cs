using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDAGGERTHROW : MonoBehaviour
{
    public bool hasShot = false;
    int shotSpeed = 12;
    GameObject Bullet;
    Rigidbody2D bulletRB;
    Vector2 newShotVector;
    Vector2 vectorToTarget;
    float currentAngle;

    public int instances = 1;
    int timesFired = 0;
    [SerializeField] AbilityParams daggerThrow;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        daggerThrow = EntityReferencerGuy.Instance.daggerThrow;

        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
        {
            shotSpeed = 8;

        }
    }

    void OnShootEffects()
    {
        timesFired++;
        if (timesFired % 3 == 0)
        {
            for (int i = 0; i < 2 * instances + 1; i++)
            {
                float currentAngle = (Mathf.PI / 6.5f) * (- 2 * instances * 0.5f + i);
                Vector2 vecToUse = new Vector2(gameObject.GetComponent<Attack>().vectorToTarget.x * Mathf.Cos(currentAngle) - gameObject.GetComponent<Attack>().vectorToTarget.y * Mathf.Sin(currentAngle), gameObject.GetComponent<Attack>().vectorToTarget.x * Mathf.Sin(currentAngle) + gameObject.GetComponent<Attack>().vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
                //Debug.Log("dagg ers! vecToUse: " + vecToUse.ToString());
                //gameObject.GetComponent<Attack>().UseAttack(daggerThrow, 2, gameObject.GetComponent<Attack>().isPlayerTeam, false, true);
                daggerThrow.UseAttack(gameObject, gameObject.GetComponent<Attack>().currentTarget, transform.position, vecToUse, gameObject.GetComponent<Attack>().isPlayerTeam, 0, false, true, true, true);
            }
        }
    }
}

    // Start is called before the first frame update
//    void Update()
//    {
//        if (gameObject.GetComponent<Attack>().timesFired % 3 == 0 && !hasShot)
//        {
//            vectorToTarget = gameObject.GetComponent<Attack>().vectorToTarget;

//            Debug.Log("Gamemaker Studio 2");

//            hasShot = true;

//            for (int i = -instances; i < instances + 1; i++)
//            {
//                currentAngle = (Mathf.PI / 10) * i;
//                GameObject newObject = Instantiate(Bullet, transform.position, transform.rotation);
//                newObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
//                newObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.dagger;
//                newObject.GetComponent<DealDamage>().damageBase = 10;
//                newObject.GetComponent<DealDamage>().owner = gameObject;
//                newObject.GetComponent<Bullet_Movement>().isPooledBullet = false;
//                newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
//                if (newObject.GetComponent<ItemBLEED>() == null)
//                {
//                    newObject.AddComponent<ItemBLEED>();
//                }
//                newObject.GetComponent<ItemBLEED>().overrideRoll = true;
//                bulletRB = newObject.GetComponent<Rigidbody2D>();
//                newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
//                bulletRB.velocity = newShotVector * shotSpeed;
//                bulletRB.simulated = true;

//                int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
//                int LayerEnemyBullet = LayerMask.NameToLayer("Enemy Bullets");
//                if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
//                {
//                    newObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.enemyBulletMaterial;
//                    newObject.layer = LayerEnemyBullet;
//                    newObject.tag = "enemyBullet";
//                }
//                else
//                {
//                    newObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.playerBulletMaterial;
//                    newObject.layer = LayerPlayerBullet;
//                    newObject.tag = "PlayerBullet";
//                }
//            }
//        }

//        if (gameObject.GetComponent<Attack>().timesFired % 3 != 0)
//        {
//            {
//                hasShot = false;
//            }
//        }
//    }

//    void Undo()
//    {
//        Destroy(this);
//    }
//}
