using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject bleedIcon;
    public GameObject poisonIcon;
    public GameObject canvas;
    public GameObject poisonSplosm;
    public GameObject electricIcon;
    public GameObject slowIcon;
    public GameObject berserkMusic;
    public GameObject berserkPlane;
    public GameObject normieFamiliar;
    public GameObject sawVisual;

    GameObject playerInstance;

    public Material playerBulletMaterial;
    public Material enemyBulletMaterial;

    public Mesh dagger;

    public bool isPaused = false;

    public int numItemsExist = 33;

    void Start()
    {
        GameObject pedestal = gameObject.GetComponent<ThirdEnemySpawner>().itemPedestal;
        numItemsExist = pedestal.GetComponent<itemPedestal>().spriteArray.GetLength(0);
        playerInstance = GameObject.Find("newPlayer");
    }

    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            EventManager.DeathEffects = null;
            //playerInstance.GetComponent<LevelUp>().levelEffects = null;
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
