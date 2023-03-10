using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFAMILIAR : MonoBehaviour
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
            gameObject.GetComponent<OtherStuff>().AddNewFamiliar(spawnedGuy, (int)ITEMLIST.FAMILIAR);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Undo()
    {
        Destroy(this);
    }
}
