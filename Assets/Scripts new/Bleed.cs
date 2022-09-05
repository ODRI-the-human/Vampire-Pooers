using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour
{
    public int timer = 0;
    public int stacks = 1;
    GameObject icon;
    GameObject spawnedIcon;

    void Start()
    {
        icon = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().bleedIcon;
        spawnedIcon = Instantiate(icon);
        spawnedIcon.GetComponent<Icons>().owner = gameObject;
    }

    void FixedUpdate()
    {
        timer++;

        if (timer % 10 == 0)
        {
            gameObject.GetComponent<HPDamageDie>().HP -= 3 * stacks;
        }

        if (timer == 100)
        {
            stacks = 0;
        }
    }
}
