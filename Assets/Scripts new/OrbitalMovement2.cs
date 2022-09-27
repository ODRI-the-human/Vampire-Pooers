using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement2 : MonoBehaviour
{
    int timer = 0;
    GameObject Player;

    void Start()
    {
        Player = gameObject.GetComponent<DealDamage>().owner;
        gameObject.GetComponent<Attack>().Crongus = Player.GetComponent<Attack>().Crongus;
    }

    void FixedUpdate()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x + 0.8f * Mathf.Sin(0.08f * timer), Player.transform.position.y + 0.8f * Mathf.Cos(0.08f * timer), Player.transform.position.z);
        timer++;

        int currentWeapon = gameObject.GetComponent<weaponType>().weaponHeld;
        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
        int ownerWeapon = owner.GetComponent<weaponType>().weaponHeld;
        gameObject.GetComponent<Attack>().fireTimerLengthMLT = owner.GetComponent<Attack>().fireTimerLengthMLT;
        if (currentWeapon != ownerWeapon)
        {
            gameObject.GetComponent<weaponType>().weaponHeld = ownerWeapon;
            gameObject.GetComponent<weaponType>().SetWeapon();
        }
    }
}
