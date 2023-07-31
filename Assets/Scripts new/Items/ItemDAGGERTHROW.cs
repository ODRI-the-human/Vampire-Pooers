//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ItemDAGGERTHROW : MonoBehaviour
//{
//    public bool hasShot = false;
//    int shotSpeed = 12;
//    GameObject Bullet;
//    Rigidbody2D bulletRB;
//    Vector2 newShotVector;
//    Vector2 vectorToTarget;
//    GameObject Player;
//    float currentAngle;
//    public int instances = 1;

//    void IncreaseInstances(string name)
//    {
//        if (name == this.GetType().ToString())
//        {
//            instances++;
//        }
//    }

//    void Start()
//    {        
//        Bullet = EntityReferencerGuy.Instance.playerBullet;

//        Player = gameObject.GetComponent<Attack>().Player;

//        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
//        {
//            shotSpeed = 8;
//        }
//    }

//    // Start is called before the first frame update
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
