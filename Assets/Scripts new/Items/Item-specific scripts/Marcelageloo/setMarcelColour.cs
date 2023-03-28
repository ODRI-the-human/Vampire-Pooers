using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMarcelColour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.gameObject.tag == "PlayerBullet")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
