using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enlargeThenShrink : MonoBehaviour
{
    float timer = 0;
    public GameObject camera;
    Vector3 distFromCam;
    float initScale;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        distFromCam = transform.position - camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position + distFromCam;
        timer += Time.deltaTime;
        if (timer < 0.2f)
        {
            transform.localScale += 50f * initScale * (0.2f - timer) * Time.deltaTime * new Vector3(1, 1, 1);
        }

        if (timer > 0.8f)
        {
            transform.localScale -= 50f * initScale * (1f - timer) * Time.deltaTime * new Vector3(1, 1, 1);
        }

        if (timer > 1f)
        {
            Destroy(gameObject);
        }
    }
}
