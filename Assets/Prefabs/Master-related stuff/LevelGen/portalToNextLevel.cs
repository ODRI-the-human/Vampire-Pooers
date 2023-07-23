using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalToNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            EntityReferencerGuy.Instance.master.GetComponent<GenerateTerrain>().ProceedToNextLevel();
        }
    }
}
