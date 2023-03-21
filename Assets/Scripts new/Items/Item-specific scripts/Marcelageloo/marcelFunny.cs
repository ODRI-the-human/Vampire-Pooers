using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcelFunny : MonoBehaviour
{
    public Sprite[] niceLetters;
    public Sprite[] evilLetters;
    public int[] letterOrders = new int[] { 0, 1, 2, 3, 4, 5, 1, 6, 4, 5, 7, 7 };
    public string[] keyThings = new string[] { "Marcel M", "Marcel A", "Marcel R", "Marcel C", "Marcel E", "Marcel L", "Marcel G", "Marcel O" };
    string nextButtonName;
    public List<GameObject> spawnedLetters = new List<GameObject>();

    public GameObject letter;
    public GameObject squarezy;
    public GameObject blackSquare;
    GameObject canvas;
    public GameObject owner;
    GameObject spawnedBlackSquare;

    public bool canType = false;

    int thingUpTo = 0;
    int spriteNo = 0;

    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        nextButtonName = keyThings[0];
        SpawnNew();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(nextButtonName) && canType)
        {
            if (thingUpTo < 11)
            {
                ChangeToEvil();
                thingUpTo++;
                SpawnNew();
            }
            else
            {
                spawnedBlackSquare = Instantiate(blackSquare, new Vector3(0, 0, -5), Quaternion.Euler(0, 0, 0));
                StartCoroutine(StartKill());
                //Invoke(nameof(DestroySquare), 0.75f);
            }
        }

        float distFromPlayer = (transform.position - owner.transform.position).magnitude;
        if (distFromPlayer < 2.5f * transform.localScale.x)
        {
            canType = true;
        }
        else
        {
            canType = false;
        }
    }

    void FixedUpdate()
    {
        Collider2D[] jimbob = Physics2D.OverlapCircleAll(transform.position, 2.5f * transform.localScale.x);
        foreach (var col in jimbob)
        {
            Debug.Log("dog things");
            if (col.gameObject.tag == "enemyBullet")
            {
                Vector3 gromble = transform.position - col.gameObject.transform.position;
                Vector2 grombley = 0.2f * new Vector2(gromble.x, gromble.y).normalized;
                col.gameObject.GetComponent<Rigidbody2D>().velocity -= grombley;
            }

            if (col.gameObject.tag == "Hostile")
            {
                col.gameObject.GetComponent<NewPlayerMovement>().isSlowed = 1;
                col.gameObject.GetComponent<NewPlayerMovement>().slowTimer = 100;
                col.gameObject.GetComponent<NewPlayerMovement>().speedDiv = 2;
            }
        }
    }

    IEnumerator StartKill()
    {
        Time.timeScale = 0.005f;

        canvas.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        float HPToRefund = 0;
        foreach (GameObject enemo in enemies)
        {
            enemo.GetComponent<HPDamageDie>().makeKillSound = false;
            enemo.GetComponent<HPDamageDie>().HP -= 2000;
            HPToRefund += 10;
        }

        GameObject[] sounds = GameObject.FindGameObjectsWithTag("audio");
        foreach (GameObject sound in sounds)
        {
            Destroy(sound);
        }

        GameObject[] enemyShots = GameObject.FindGameObjectsWithTag("enemyBullet");
        foreach (GameObject bulletter in enemyShots)
        {
            if (!bulletter.GetComponent<DealDamage>().isSourceBullet)
            {
                Destroy(bulletter);
            }
        }

        yield return new WaitForSecondsRealtime(0.75f);

        Time.timeScale = 1;
        canvas.SetActive(true);
        Destroy(gameObject);
        Destroy(spawnedBlackSquare);
        owner.GetComponent<Healing>().Healo(HPToRefund);
        foreach (GameObject Letter in spawnedLetters)
        {
            Destroy(Letter);
        }
    }

    void SpawnNew()
    {
        int i = 0;
        foreach (GameObject letter in spawnedLetters)
        {
            letter.GetComponent<letterPositioning>().properPos = gameObject.transform.position + new Vector3(0.5f * (i - 0.5f * thingUpTo), 1.5f, -5);
            i++;
        }

        GameObject spawned = Instantiate(letter, gameObject.transform.position + new Vector3(0.5f * (i - 0.5f * thingUpTo), 1.5f, -5), Quaternion.Euler(0, 0, 0)); // MARCELAGELOO
        spawnedLetters.Add(spawned);   //            012345164577

        int letterNumber = letterOrders[thingUpTo];
        nextButtonName = keyThings[letterNumber];
        spawned.GetComponent<SpriteRenderer>().sprite = niceLetters[letterNumber];
    }

    void ChangeToEvil()
    {
        GameObject squarer = Instantiate(squarezy);
        squarer.GetComponent<AudioSource>().volume = 0;
        int letterNumber = letterOrders[thingUpTo];
        spawnedLetters[thingUpTo].GetComponent<SpriteRenderer>().sprite = evilLetters[letterNumber];
        spawnedLetters[thingUpTo].GetComponent<letterPositioning>().isEvil = true;
    }
}
