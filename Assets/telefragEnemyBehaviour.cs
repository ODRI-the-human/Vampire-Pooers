using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class telefragEnemyBehaviour : MonoBehaviour
{
    public int timer = 0;
    Vector3 posToTP;
    float prevSpeed;

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

        if (timer == Mathf.Round(rateMult * 185)) /// gameObject.GetComponent<Attack>().stopwatchDebuffAmount))
        {
            prevSpeed = gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed;
            gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = 0;
            SoundManager.Instance.PlaySound(EntityReferencerGuy.Instance.telefragWarn);
            posToTP = gameObject.GetComponent<Attack>().currentTarget.transform.position;
            spawnedWarn = Instantiate(EntityReferencerGuy.Instance.targetWarn, posToTP, Quaternion.Euler(0, 0, 0));
            spawnedWarn.GetComponent<ownerDestroy>().owner = gameObject;
        }

        if (timer == Mathf.Round(rateMult * 225))// / gameObject.GetComponent<Attack>().stopwatchDebuffAmount))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            transform.position = posToTP;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            SoundManager.Instance.PlaySound(EntityReferencerGuy.Instance.telefragDo);
            Destroy(spawnedWarn);
            timer = 0;
            gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = prevSpeed;
        }
    }
}
