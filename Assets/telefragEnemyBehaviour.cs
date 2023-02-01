using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telefragEnemyBehaviour : MonoBehaviour
{
    public int timer = 0;
    Vector3 posToTP;
    float prevSpeed;

    public GameObject warnAudio;
    public GameObject doAudio;
    public GameObject targetWarn;
    GameObject spawnedWarn;

    float rateMult;

    // Start is called before the first frame update
    void Start()
    {
        prevSpeed = gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed;
        rateMult = Random.Range(0.9f, 1.1f);
    }

    void FixedUpdate()
    {
        timer++;

        if (timer == Mathf.Round(rateMult * 175 / gameObject.GetComponent<Attack>().stopwatchDebuffAmount))
        {
            prevSpeed = gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed;
            gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = 0;
            Instantiate(warnAudio);
            posToTP = gameObject.GetComponent<Attack>().currentTarget.transform.position;
            spawnedWarn = Instantiate(targetWarn, posToTP, Quaternion.Euler(0, 0, 0));
            spawnedWarn.GetComponent<ownerDestroy>().owner = gameObject;
        }

        if (timer == Mathf.Round(rateMult * 225 / gameObject.GetComponent<Attack>().stopwatchDebuffAmount))
        {
            transform.position = posToTP;
            Instantiate(doAudio);
            Destroy(spawnedWarn);
        }
        
        if (timer == Mathf.Round(rateMult * 250 / gameObject.GetComponent<Attack>().stopwatchDebuffAmount))
        {
            timer = 0;
            gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = prevSpeed;
        }
    }
}
