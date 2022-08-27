using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityReferencerGuy : MonoBehaviour
{
    public GameObject ATGMissile;
    public GameObject ATGMissileHostile;
    public GameObject Player;
    public GameObject wapantCircle;
    public GameObject wapantCircleHostile;
    public GameObject Creep;
    public GameObject CreepHostile;
    public GameObject dodgeSplosion;
    public GameObject orbSkothos;
    public GameObject orbSkothos2;
    public GameObject playerBullet;
    public GameObject enemyBullet;
    public GameObject contactMan;

    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
