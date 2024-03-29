using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFOURDIRMARTY : ItemScript
{
    int noShots = 0;
    int timesFired = 0;

    public override void OnPrimaryUse()
    {
        timesFired++;
        if (timesFired % 4 == 0)
        {
            noShots = 4 * instances - 1;

            for (int i = 0; i < noShots; i++)
            {
                float currentAngle = Mathf.PI / (2 * instances) * i + Mathf.PI / (2 * instances);
                Vector2 vecToUse = new Vector2(gameObject.GetComponent<Attack>().vectorToTarget.x * Mathf.Cos(currentAngle) - gameObject.GetComponent<Attack>().vectorToTarget.y * Mathf.Sin(currentAngle), gameObject.GetComponent<Attack>().vectorToTarget.x * Mathf.Sin(currentAngle) + gameObject.GetComponent<Attack>().vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
                //Debug.Log("dagg ers! vecToUse: " + vecToUse.ToString());
                //gameObject.GetComponent<Attack>().UseAttack(daggerThrow, 2, gameObject.GetComponent<Attack>().isPlayerTeam, false, true);
                gameObject.GetComponent<Attack>().abilityTypes[0].UseAttack(gameObject, gameObject.GetComponent<Attack>().currentTarget, transform.position, vecToUse, gameObject.GetComponent<Attack>().isPlayerTeam, 0, false, true, false, true);
            }
        }
    }
}
