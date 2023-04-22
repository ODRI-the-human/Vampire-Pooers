using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWAPANT : MonoBehaviour
{
    float wapantTimer = 0;
    public float wapantTimerLength = 150f;
    public int instances = 1;
    public GameObject wapantCircle;
    bool isActive = false;

    void Start()
    {
        wapantCircle = EntityReferencerGuy.Instance.wapantCircle;

        if (gameObject.tag == "Player")
        {
            wapantCircle = EntityReferencerGuy.Instance.wapantCircle;
            isActive = true;
        }

        else
        {
            if (gameObject.tag == "Hostile")
            {
                wapantCircle = EntityReferencerGuy.Instance.wapantCircleHostile;
                isActive = true;
            }
        }
    }

    void FixedUpdate()
    {
        wapantTimer--;

        if (wapantTimer < 1 && isActive)
        {
            wapantTimer = wapantTimerLength / (1.25f * instances);
            GameObject Fatty = Instantiate(wapantCircle, transform.position, transform.rotation);
            Fatty.transform.localScale *= 0.5f + 0.3f * instances;
            Fatty.transform.position = new Vector3(Fatty.transform.position.x, Fatty.transform.position.y, 0);
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
