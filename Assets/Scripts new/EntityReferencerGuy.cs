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

    public GameObject playerInstance;

    public Material playerBulletMaterial;
    public Material enemyBulletMaterial;

    public Mesh dagger;

    public int numItemsExist = 33;


    public float timeLeft = 240;
    bool timerActive = true;


    void Start()
    {
        GameObject pedestal = gameObject.GetComponent<ThirdEnemySpawner>().itemPedestal;
        numItemsExist = pedestal.GetComponent<itemPedestal>().spriteArray.GetLength(0);
        playerInstance = GameObject.Find("newPlayer");
        //Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (timeLeft < 0 && timerActive)
        {
            GameObject oopsers = Instantiate(neutralExplosion);
            oopsers.transform.localScale *= 100;
            Destroy(gameObject.GetComponent<StatsText>().timeText);
            timerActive = false;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
