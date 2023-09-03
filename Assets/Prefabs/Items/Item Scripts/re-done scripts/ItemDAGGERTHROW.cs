using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDAGGERTHROW : ItemScript
{
    public bool hasShot = false;
    int shotSpeed = 12;
    GameObject Bullet;
    Rigidbody2D bulletRB;
    Vector2 newShotVector;
    Vector2 vectorToTarget;
    float currentAngle;

    int timesFired = 0;
    [SerializeField] AbilityParams daggerThrow;

    void Start()
    {
        daggerThrow = EntityReferencerGuy.Instance.daggerThrow;

        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
        {
            shotSpeed = 8;

        }
    }

    public override void OnPrimaryUse()
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