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
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        Vector3 vec3 = Vector3.zero;

        if (Player.tag == "Player")
        {
            vec3 = Player.GetComponent<Attack>().reticle.transform.position - transform.position;
            gameObject.GetComponent<Attack>().isFiring = false;
        }
        else
        {
            vec3 = Player.GetComponent<Attack>().currentTarget.transform.position - transform.position;
            gameObject.GetComponent<Attack>().isFiring = false;
            gameObject.GetComponent<Attack>().shotSpeed = 4;
        }
        gameObject.GetComponent<Attack>().vectorToTarget = new Vector2(vec3.x, vec3.y).normalized;

        transform.position = new Vector3(Player.transform.position.x + distanceFromPlayer * Mathf.Sin(0.03f * (timer + timerDelay)), Player.transform.position.y + distanceFromPlayer * Mathf.Cos(0.03f * (timer + timerDelay)), Player.transform.position.z);
        timer += Time.deltaTime * 60;

        //Debug.Log("Orb vec/player vec: " + gameObject.GetComponent<Attack>().vectorToTarget.ToString() + "/" + Player.GetComponent<Attack>().vectorToTarget.ToString());

        int currentWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
        int ownerWeapon = owner.GetComponent<ItemHolder>().weaponHeld;
        gameObject.GetComponent<Attack>().fireTimerLengthMLT = owner.GetComponent<Attack>().fireTimerLengthMLT;
        gameObject.GetComponent<Attack>().Crongus = owner.GetComponent<Attack>().Crongus;
        gameObject.GetComponent<Attack>().damageBonus = owner.GetComponent<Attack>().damageBonus;
        if (currentWeapon != ownerWeapon)
        {
            gameObject.GetComponent<weaponType>().SetWeapon(ownerWeapon);
        }

        if (gameObject.GetComponent<Attack>().Bullet == null)
        {
            gameObject.GetComponent<Attack>().Bullet = Player.GetComponent<Attack>().Bullet;
            gameObject.GetComponent<DealDamage>().finalDamageMult = 0.25f;
        }
    }
}
