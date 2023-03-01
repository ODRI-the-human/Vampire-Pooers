using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawRotation : MonoBehaviour
{
    public Vector3 offset;
    public GameObject owner;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        offset = (target.transform.position - owner.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = owner.transform.position + 3 * offset;
        transform.rotation *= Quaternion.Euler(0, 0, 240 * Time.deltaTime);
        owner.transform.position += offset * 7.5f * Time.deltaTime;

        if (owner == null)
        {
            Destroy(gameObject);
        }
    }
}
