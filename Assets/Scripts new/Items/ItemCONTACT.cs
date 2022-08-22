using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONTACT : MonoBehaviour
{
    public int instances = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
        {
            GameObject Bingus = Instantiate(GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().contactMan);
            Bingus.tag = gameObject.tag;
            Bingus.GetComponent<dieOnContactWithBullet>().master = gameObject;
            Bingus.GetComponent<dieOnContactWithBullet>().instances = instances;
        }
    }
}
