using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    public bool runStart = true;

    void Awake()
    {
        if (!gameObject.GetComponent<DealDamage>().isBulletClone)
        {
            gameObject.GetComponent<DealDamage>().finalDamageDIV += 4;
            gameObject.GetComponent<DealDamage>().massCoeff /= 4;
        }
    }

    void Start()
    {
        if (gameObject.GetComponent<Attack>() != null)
        {
            gameObject.GetComponent<Attack>().fireTimerDIV += 4;
            GameObject farter = gameObject.GetComponent<DealDamage>().master;
            gameObject.GetComponent<Attack>().PlayerShootAudio = farter.GetComponent<EntityReferencerGuy>().soyShotAudio;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV -= 4;
        gameObject.GetComponent<DealDamage>().massCoeff *= 4;
        if (gameObject.GetComponent<Attack>() != null)
        {
            gameObject.GetComponent<Attack>().fireTimerDIV -= 4;
            GameObject farter = gameObject.GetComponent<DealDamage>().master;
            gameObject.GetComponent<Attack>().PlayerShootAudio = farter.GetComponent<EntityReferencerGuy>().normieShotAudio;
        }

        Destroy(this);

        Debug.Log("Foodland Sale on NOW!!!!!!!!");
    }
}
