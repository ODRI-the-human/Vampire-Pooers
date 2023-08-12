using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableItemDescriptions : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<getItemDescription>() != null)
        {
            col.gameObject.GetComponent<getItemDescription>().itemsExist++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<getItemDescription>() != null)
        {
            col.gameObject.GetComponent<getItemDescription>().itemsExist--;
        }
    }

    void Start()
    {
        Invoke(nameof(Kill), 5f);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
