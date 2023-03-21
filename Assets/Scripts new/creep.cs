using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creep : MonoBehaviour
{
    public int destroyDelay = 5;
    public Sprite[] sprites;

    void Start()
    {
        Invoke(nameof(DestorySelf), destroyDelay); //will invoke (run the function) in so many seconds
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];
        transform.rotation = Quaternion.Euler(0, 0, 90 * Random.Range(0, 4));
        gameObject.GetComponent<DealDamage>().damageType = (int)DAMAGETYPES.POISON;
    }

    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
