using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement2 : MonoBehaviour
{
    public float timer = 0;
    GameObject Player;
    public float timerDelay;
    public float distanceFromPlayer = 2;

    void Start()
    {
        Player = gameObject.GetComponent<DealDamage>().owner;
        gameObject.GetComponent<Attack>().Crongus = Player.GetComponent<Attack>().Crongus;
        if (gameObject.tag == "enemyBullet")
        {
            int LayerEnemy = LayerMask.NameToLayer("HitEnemBulletsAndEnemies");
            gameObject.layer = LayerEnemy;
        }
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x + distanceFromPlayer * Mathf.Sin(0.03f * (timer + timerDelay)), Player.transform.position.y + distanceFromPlayer * Mathf.Cos(0.03f * (timer + timerDelay)), Player.transform.position.z);
        timer += Time.deltaTime * 60;

        int currentWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
        int ownerWeapon = owner.GetComponent<weaponType>().weaponHeld;
        gameObject.GetComponent<Attack>().fireTimerLengthMLT = owner.GetComponent<Attack>().fireTimerLengthMLT;
        gameObject.GetComponent<Attack>().Crongus = owner.GetComponent<Attack>().Crongus;
        gameObject.GetComponent<Attack>().levelDamageBonus = owner.GetComponent<Attack>().levelDamageBonus;
        if (currentWeapon != ownerWeapon)
        {
            gameObject.GetComponent<weaponType>().weaponHeld = ownerWeapon;
            gameObject.GetComponent<weaponType>().SetWeapon();
        }
    }
}
