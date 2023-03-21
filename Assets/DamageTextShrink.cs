using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextShrink : MonoBehaviour
{
    int timer = 25;
    float randX;
    float randY;
    Color tmp;

    void Start()
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>().color;
        randX = Random.Range(-0.25f, 0.25f);
        randY = Random.Range(-0.25f, 0.25f);
    }

    void Update()
    {
        transform.position += Time.deltaTime * timer/10 * new Vector3(randX, randY, 0);
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
