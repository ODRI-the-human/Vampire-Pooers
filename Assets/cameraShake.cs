using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    // Update is called once per frame but idk if this ienumerator is
    public IEnumerator Shake (float timer)
    {
        if (timer > 1)
        {
            timer /= 1f;
            transform.localPosition = new Vector3(0, 0, -10.6f);
            transform.localPosition += new Vector3(Random.Range(-0.5f, 0.5f) * timer, Random.Range(-0.5f, 0.5f) * timer, Random.Range(-0.5f, 0.5f) * timer);
        }
        else
        {
            timer = 0;
        }

        yield return null;
    }
}
