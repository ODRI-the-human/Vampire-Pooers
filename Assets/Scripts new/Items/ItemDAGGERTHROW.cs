using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDAGGERTHROW : MonoBehaviour
{
    bool hasShot = false;
    int shotSpeed = 12;
    GameObject Bullet;
    Rigidbody2D bulletRB;
    Vector2 newShotVector;
    Vector2 vectorToTarget;
    GameObject Player;
    float currentAngle;

    public int instances = 1;

    void Start()
    {
        Bullet = gameObject.GetComponent<Attack>().Bullet;

    }

    // Start is called before the first frame update
    void Update()
    {
        if (gameObject.GetComponent<Attack>().timesFired % 3 == 0 && !hasShot)
        {
            switch (gameObject.GetComponent<Attack>().playerControlled)
            {
                case true:
                    vectorToTarget = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
                    break;
                case false:
                    vectorToTarget = (Player.transform.position - gameObject.transform.position).normalized;
                    break;
            }

            Debug.Log("Gamemaker Studio 2");

            hasShot = true;

            for (int i = 0; i < 3; i++)
            {
                currentAngle = (Mathf.PI / 10) * (i + 1) - 2 * (Mathf.PI / 10);
                GameObject newObject = Instantiate(Bullet, transform.position, transform.rotation);
                newObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                newObject.GetComponent<DealDamage>().damageBase = 10 * instances;
                newObject.AddComponent<ItemBLEED>();
                newObject.GetComponent<ItemBLEED>().instances = 20;
                bulletRB = newObject.GetComponent<Rigidbody2D>();
                newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
                bulletRB.velocity = newShotVector * shotSpeed;
            }
        }
        
        if (gameObject.GetComponent<Attack>().timesFired % 3 != 0)
        {
            {
                hasShot = false;
            }
        }
    }
}