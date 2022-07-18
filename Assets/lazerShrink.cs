using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerShrink : MonoBehaviour
{

    int timer = 0;
    Vector2 scaleChange;
    public Collider2D collider;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        scaleChange = new Vector3(1,1/(0.5f + 0.5f * timer),1);
        if (scaleChange.y < 0.1f)
        {
            Destroy(gameObject);
        }
        transform.localScale = scaleChange;

        if (timer > 4)
        {
            Destroy(collider);
        }
    }
}
