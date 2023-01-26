using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerSpin : MonoBehaviour
{
    float timer = 0;

    void Update()
    {
        float distance = (gameObject.GetComponent<Attack>().currentTarget.transform.position - transform.position).magnitude;
        distance = Mathf.Pow(distance, 2.8f);

        timer += distance * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, timer);
    }
}
