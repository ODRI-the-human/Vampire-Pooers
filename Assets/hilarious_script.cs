using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hilarious_script : MonoBehaviour
{
    int timer = 0;
    float timerMario = 400;
    public GameObject FAT;

    // Update is called once per frame BUT FIXEDUPDATE ISN'T???? WHAT????????????????????????? DIFFERENT THINGS DO DIFFERENT THINGS????????????????????? THE FUCK?
    void FixedUpdate()
    {
        timer++;
        //Time.timeScale = 2.5f + 2 * Mathf.Sin(timer*timerMario);

        //if (Random.Range(0, 50) > 45)
        //{
        //    Time.timeScale = 99;
        //    timerMario = 1f;
        //}

        //if (Random.Range(0, 50) < 5)
        //{
        //    Time.timeScale = .5f;
        //    timerMario = 40000000f;
        //}

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f + 0.5f * Mathf.Sin(timer * timerMario), 0.5f + 0.5f * Mathf.Sin(timer * 2), 0.5f + 0.5f * Mathf.Sin(timer), 1);
    }
}
