using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPMoveTowardsPlayer : MonoBehaviour
{
    public GameObject target;
    public float timer = 0;

    public bool taken = false;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Mathf.Pow(timer / 10, 3));
            timer += 15 * Time.deltaTime;
        }
    }
}
