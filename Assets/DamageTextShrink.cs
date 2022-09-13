using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextShrink : MonoBehaviour
{
    int timer = 25;
    Color tmp;

    void Start()
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>().color;
    }

    void FixedUpdate()
    {
        if (timer < 6)
        {
            tmp.a -= 0.2f;
            gameObject.GetComponent<TextMeshProUGUI>().color = tmp;
        }

        timer--;
    }
}
