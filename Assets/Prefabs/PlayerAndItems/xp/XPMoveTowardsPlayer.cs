using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPMoveTowardsPlayer : MonoBehaviour
{
    public GameObject target;
    public float timer = 0;
    int destroyTimer = 0;

    public bool taken = false;
    public bool isVisible = true;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Mathf.Pow(timer / 10, 3));
            timer += 15 * Time.deltaTime;
        }
        Debug.Log(Screen.currentResolution.ToString());
    }

    void FixedUpdate()
    {
        destroyTimer++;

        if (destroyTimer % 2 == 0 && destroyTimer >= 150)
        {
            if (isVisible)
            {
                isVisible = false;
                Color funny = gameObject.GetComponent<SpriteRenderer>().color;
                funny.a = 0;
                gameObject.GetComponent<SpriteRenderer>().color = funny;
            }
            else
            {
                isVisible = true;
                Color funny = gameObject.GetComponent<SpriteRenderer>().color;
                funny.a = 1;
                gameObject.GetComponent<SpriteRenderer>().color = funny;
            }
        }

        if (destroyTimer > 200)
        {
            Destroy(gameObject);
        }
    }
}
