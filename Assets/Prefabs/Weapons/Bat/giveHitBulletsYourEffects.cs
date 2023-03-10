using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveHitBulletsYourEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject master = gameObject.GetComponent<dieOnContactWithBullet>().master;
        master.GetComponent<faceInFunnyDirection>().ApplyContact(col.gameObject);
    }
}
