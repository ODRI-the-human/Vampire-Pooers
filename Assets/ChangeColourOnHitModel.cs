using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeColourOnHitModel : MonoBehaviour
{
    float iFrames = 0;
    public Material material;
    Color originalColor;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("newPlayer");
        originalColor = material.color;
    }

    void FixedUpdate()
    {
        iFrames = Player.GetComponent<HPDamageDie>().iFrames;

        Color tmp = material.color;
        if (iFrames > 0)
        {
            if (iFrames % 2 == 0)
            {
                tmp = Color.black;
                material.color = tmp;
            }
            else
            {
                tmp = originalColor;
                material.color = tmp;
            }
        }
        else
        {
            tmp = originalColor;
            material.color = tmp;
        }
    }
}
