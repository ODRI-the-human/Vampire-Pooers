using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOMINGFAMILIAR : MonoBehaviour
{
    GameObject master;
    GameObject normieFamiliar;
    GameObject spawnedGuy;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            master = GameObject.Find("bigFuckingMasterObject");
            normieFamiliar = master.GetComponent<EntityReferencerGuy>().normieFamiliar;
            spawnedGuy = Instantiate(normieFamiliar, transform.position, transform.rotation);
            gameObject.GetComponent<OtherStuff>().AddNewFamiliar(spawnedGuy, (int)ITEMLIST.HOMINGFAMILIAR);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}