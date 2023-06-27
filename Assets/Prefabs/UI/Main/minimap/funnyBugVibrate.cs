using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funnyBugVibrate : MonoBehaviour
{
    Vector3 position;
    public GameObject player;
    public GameObject newBarry;
    bool isPlaying = false;
    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("actualPlayer");

        float appearRoll = Random.Range(0f, 100f);
        if (appearRoll > 98f)
        {
            transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            while ((transform.position - player.transform.position).magnitude < 10)
            {
                transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            position += (player.transform.position - position).normalized * Time.deltaTime * 7;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }

        transform.position = position;
        float distance = (player.transform.position - transform.position).magnitude;
        if (distance < 7)
        {
            float shakeAmt = 0.5f - distance / 14;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            transform.position += new Vector3(Random.Range(-shakeAmt, shakeAmt), Random.Range(-shakeAmt, shakeAmt), 0);
            gameObject.GetComponent<AudioSource>().pitch = (7 - distance) / 14 + 0.2f;
            if (!isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            isPlaying = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            if (isPlaying && !isChasing)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
            isPlaying = false;
        }

        if (distance < 3)
        {
            isChasing = true;
            if (!isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            isPlaying = true;
            gameObject.GetComponent<AudioDistortionFilter>().enabled = true;
        }

        if (distance < 0.5f)
        {
            EntityReferencerGuy.Instance.master.GetComponent<playerManagement>().barry63 = newBarry;
            GameObject deathStatObject = GameObject.Find("keepsBestScore");
            deathStatObject.GetComponent<keepBestScore>().doRandomiseMeme = false;
            deathStatObject.GetComponent<keepBestScore>().bullyText.text = "ALL HAIL HUMPHREY, LORD OF COSMOS AND HARBINGER OF ANNIHILATION";
            deathStatObject.GetComponent<keepBestScore>().resetText.text = "PRESS R TO SURRENDER YOUR SOUL";
            player.GetComponent<HPDamageDie>().Hurty(2147483647.001f, true, false, 0, 0, true, null);
            gameObject.GetComponent<AudioDistortionFilter>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }
}
