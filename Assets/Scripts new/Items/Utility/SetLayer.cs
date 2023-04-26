using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
        int LayerEnemyBullet = LayerMask.NameToLayer("Enemy Bullets");

        if (gameObject.tag == "enemyBullet")
        {
            gameObject.layer = LayerEnemyBullet;
        }
        else
        {
            gameObject.layer = LayerPlayerBullet;
        }
    }
}
