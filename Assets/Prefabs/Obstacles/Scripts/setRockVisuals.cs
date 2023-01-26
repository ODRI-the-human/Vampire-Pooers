using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRockVisuals : MonoBehaviour
{
    public GameObject tnt;
    public float pooper;

    // Start is called before the first frame update
    void Start()
    {
        int bumBoy = Random.Range(0, 4);

        transform.rotation = Quaternion.Euler(180, 0, 90 * bumBoy);

        pooper = Random.Range(0f, 1f);
        if (pooper > 0.9f)
        {
            Instantiate(tnt, transform.position, Quaternion.Euler(180, 0, 180));
            Destroy(gameObject);
        }
    }
}
