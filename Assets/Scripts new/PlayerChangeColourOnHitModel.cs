using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColourOnHitModel : MonoBehaviour
{
    public Material material;

    void FixedUpdate()
    {
        Color tmp;
        material.color = Color.white;

        if (gameObject.GetComponent<HPDamageDie>().iFrames > 0)
        {
            if (gameObject.GetComponent<HPDamageDie>().iFrames % 2 == 0)
            {
                tmp = Color.black;
                material.color = tmp;
            }
            else
            {
                tmp = Color.white;
                material.color = tmp;
            }
        }
    }
}
