using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    float timer = 0;
    public float timerDelay;
    GameObject Player;

    void Start()
    {
        Player = gameObject.GetComponent<DealDamage>().owner;
        if (gameObject.tag == "enemyBullet")
        {
            int LayerEnemy = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
            gameObject.layer = LayerEnemy;
        }
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x + 2.3f * Mathf.Sin(0.0175f * (timer + timerDelay)), Player.transform.position.y + 2.3f * Mathf.Cos(0.0175f * (timer + timerDelay)), Player.transform.position.z);
        timer += Time.deltaTime * 60;
    }
}
