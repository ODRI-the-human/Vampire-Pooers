using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tntExplodeOnDeath : MonoBehaviour
{
    public GameObject neutralExplosion;

    public void ApplyOwnOnDeaths()
    {
        Instantiate(neutralExplosion, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
