using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : MonoBehaviour
{
    public bool runStart = true;

    void Awake()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV += 4;
        gameObject.GetComponent<DealDamage>().massCoeff /= 4;
    }

    void Start()
    {
        if (gameObject.GetComponent<Attack>() != null)
        {
            gameObject.GetComponent<Attack>().fireTimerDIV += 4;
            gameObject.GetComponent<Attack>().PlayerShootAudio = EntityReferencerGuy.Instance.soyShotAudio;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().finalDamageDIV -= 4;
        gameObject.GetComponent<DealDamage>().massCoeff *= 4;
        if (gameObject.GetComponent<Attack>() != null)
        {
            gameObject.GetComponent<Attack>().fireTimerDIV -= 4;
            gameObject.GetComponent<Attack>().PlayerShootAudio = EntityReferencerGuy.Instance.normieShotAudio;
        }

        Destroy(this);

        Debug.Log("Foodland Sale on NOW!!!!!!!!");
    }
}
