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
    public GameObject statusIcon;
    public GameObject canvas;
    public GameObject canvasInnerBound;
    public GameObject poisonSplosm;
    public GameObject berserkMusic;
    public GameObject berserkPlane;
    public GameObject darkArtSword;
    public GameObject normieFamiliar;
    public GameObject sawVisual;
    public GameObject soyShotAudio;
    public GameObject normieShotAudio;
    public GameObject neutralExplosion;
    public GameObject camera;
    public GameObject marcelageloo;
    public GameObject regularShotParticle;
    public GameObject empty;
    public GameObject batHitbox;
    public GameObject darkArtHitbox;
    public GameObject chargeBar;
    public GameObject reticle;
    public GameObject GameManager;

    public Sprite[] itemSprites;
    public Sprite[] weaponSprites;

    public GameObject master;

    public GameObject bat;
    public GameObject darkArt;

    int bosNumToSpawn = 1;


    public GameObject playerInstance;

    public Material playerBulletMaterial;
    public Material enemyBulletMaterial;

    public Mesh dagger;
    public Mesh saw;
    public Mesh bullet;

    public int numItemsExist;

    bool timerActive = true;
    [HideInInspector] public float time = 180;

    public GameObject keepBestStatObj;
    bool playerHasDied = false;

    public GameObject itemsHeldVisualiser;

    public int numVisibleWalls = 0; // This is just a little variable for looking at obstacles' onbecomevisible and stuff.



    // Enemy shit
    public AudioClip telefragWarn;
    public AudioClip telefragDo;
    public GameObject targetWarn;



    // Any global variables, like the stopwatchdebuff amount, should go here!
    [System.NonSerialized] public float stopWatchDebuffAmt = 1;






    // Any scriptableobjects that need to be referenced globally go here.
    public AbilityParams daggerThrow;
    public AbilityParams berserkAttack;




    public static EntityReferencerGuy Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //camera = GameObject.Find("Main Camera");
        //canvas = GameObject.Find("Canvas");
        //canvasInnerBound = GameObject.Find("Bound Box");
    }

    void Start()
    {
        numItemsExist = (int)ITEMLIST.CREEPSHOT;
        //playerInstance = GameObject.Find("newPlayer");
        //Application.targetFrameRate = 60;

        DontDestroyOnLoad(keepBestStatObj);
    }

    void BeganNewWave()
    {
        if (itemsHeldVisualiser != null)
        {
            itemsHeldVisualiser.GetComponent<itemVisualiser>().UpdateVisual();
        }
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 180;
        }
    }
}
