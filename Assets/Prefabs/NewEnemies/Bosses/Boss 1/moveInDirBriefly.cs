using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveInDirBriefly : MonoBehaviour
{
    int timer = 0;

    public Vector3 offset;
    public GameObject owner;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= 3 * Time.deltaTime * (target.transform.position - owner.transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (timer > 12)
        {
            Destroy(this);
        }

        timer++;
    }
}
