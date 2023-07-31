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
        gameObject.GetComponent<DealDamage>().finalDamageMult = 0.25f;
        gameObject.GetComponent<Attack>().attackAutomatically = false;
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x + distanceFromPlayer * Mathf.Sin(0.03f * (timer + timerDelay)), Player.transform.position.y + distanceFromPlayer * Mathf.Cos(0.03f * (timer + timerDelay)), Player.transform.position.z);
        timer += Time.deltaTime * 60;

        //Debug.Log("Orb vec/player vec: " + gameObject.GetComponent<Attack>().vectorToTarget.ToString() + "/" + Player.GetComponent<Attack>().vectorToTarget.ToString());

        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
        gameObject.GetComponent<DealDamage>().damageBonus = owner.GetComponent<DealDamage>().damageBonus;
        //if (currentWeapon != ownerWeapon)
        //{
        //    gameObject.GetComponent<weaponType>().SetWeapon(ownerWeapon);
        //}
    }
}
