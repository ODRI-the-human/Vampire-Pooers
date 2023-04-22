using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAUTOFAMILIAR : MonoBehaviour
{
    GameObject normieFamiliar;
    GameObject spawnedGuy;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            normieFamiliar = EntityReferencerGuy.Instance.normieFamiliar;
            spawnedGuy = Instantiate(normieFamiliar, transform.position, transform.rotation);
            spawnedGuy.GetComponent<Attack>().playerControlled = false;
            spawnedGuy.GetComponent<DealDamage>().damageBase = 15;
            gameObject.GetComponent<OtherStuff>().AddNewFamiliar(spawnedGuy, (int)ITEMLIST.AUTOFAMILIAR);
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
