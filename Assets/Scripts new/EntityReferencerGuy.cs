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
    public GameObject soyShotAudio;
    public GameObject neutralExplosion;
    public GameObject camera;


    public GameObject boss; //SHOULD JUST BE A TEMPORARY SOLUTION. BOSS SPAWNING SHOULD BE HANDLED BY THE SPAWNER.
    int bosNumToSpawn = 1;


    public GameObject playerInstance;

    public Material playerBulletMaterial;
    public Material enemyBulletMaterial;

    public Mesh dagger;
    public Mesh saw;

    public int numItemsExist = 33;

    bool timerActive = true;
    [HideInInspector] public float time = 180;

    public GameObject keepBestStatObj;
    bool playerHasDied = false;

    void Start()
    {
        GameObject pedestal = gameObject.GetComponent<ThirdEnemySpawner>().itemPedestal;
        numItemsExist = pedestal.GetComponent<itemPedestal>().spriteArray.GetLength(0);
        playerInstance = GameObject.Find("newPlayer");
        //Application.targetFrameRate = 60;

        DontDestroyOnLoad(keepBestStatObj);
    }

    void Update()
    {
        if (time < 0)
        {
            for (int i = 0; i < bosNumToSpawn; i++)
            {
                Instantiate(boss, transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0), Quaternion.Euler(0, 0, 0));
            }
            gameObject.GetComponent<ThirdEnemySpawner>().spawnTimer = 999999999999;
            bosNumToSpawn++;
            time = 180;
        }

        if (gameObject.GetComponent<ThirdEnemySpawner>().enemiesAreSpawning)
        {
            time -= Time.deltaTime;
        }

        if (playerInstance == null && !playerHasDied)
        {
            keepBestStatObj.GetComponent<keepBestScore>().ShowStats();
            playerHasDied = true;
        }
    }
}
