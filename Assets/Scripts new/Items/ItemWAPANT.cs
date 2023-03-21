using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWAPANT : MonoBehaviour
{
    float wapantTimer = 0;
    public float wapantTimerLength = 150f;
    public int instances = 1;
    public GameObject wapantCircle;
    public GameObject MasterObject;
    bool isActive = false;

    void Start()
    {
        MasterObject = GameObject.Find("bigFuckingMasterObject");
        wapantCircle = MasterObject.GetComponent<EntityReferencerGuy>().wapantCircle;

        if (gameObject.tag == "Player")
        {
            wapantCircle = MasterObject.GetComponent<EntityReferencerGuy>().wapantCircle;
            isActive = true;
        }

        else
        {
            if (gameObject.tag == "Hostile")
            {
                wapantCircle = MasterObject.GetComponent<EntityReferencerGuy>().wapantCircleHostile;
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
            GameObject Fatty = Instantiate(wapantCircle, transform.position + new Vector3(0, 0, 0), transform.rotation);
            Fatty.transform.localScale *= 0.5f + 0.3f * instances;
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
