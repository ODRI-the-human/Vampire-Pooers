using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBulletMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "PlayerBullet")
        {
            gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.playerBulletMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.enemyBulletMaterial;
        }
    }
}
