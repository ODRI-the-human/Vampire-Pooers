using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestThings : MonoBehaviour
{
    Vector3 positionToLand;
    float timeSinceStart = 0;
    public Collider2D hitBox;
    public GameObject forceFieldObj;
    public GameObject forceFieldInstance;
    public GameObject pedestalObj;
    public GameObject[] pedestalInstances;
    public GameObject selector; // Is set by the mouse item selector, and it used to show the lil selection process.

    // Start is called before the first frame update
    void Start()
    {
        pedestalInstances = new GameObject[3];
        positionToLand = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Invoke(nameof(HitFloor), 1f);
    }

    void HitFloor()
    {
        Debug.Log("dongus");
        EntityReferencerGuy.Instance.camera.GetComponent<cameraMovement>().CameraShake(0, new Vector3(0, -500, 0));
        hitBox.enabled = true;
        Invoke(nameof(DisableHitBox), 0.1f);
        Invoke(nameof(EnableForceField), 1f);
    }

    void DisableHitBox()
    {
        hitBox.enabled = false;
    }

    void EnableForceField()
    {
        forceFieldInstance = Instantiate(forceFieldObj, transform.position, Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            pedestalInstances[i] = Instantiate(pedestalObj, transform.position + new Vector3(2, 0, 0) * (-1f + i) - new Vector3(0, 0, 1), Quaternion.identity);
            pedestalInstances[i].transform.SetParent(transform);
        }
    }

    public void KillItems(GameObject collector) // Collector is the object to pick an item.
    {
        foreach (GameObject pedestal in pedestalInstances)
        {
            Destroy(pedestal);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = positionToLand + (1 - timeSinceStart) * new Vector3(0, 150, 0);
        timeSinceStart = Mathf.Clamp(timeSinceStart + Time.deltaTime, 0, 1);
        if (selector != null)
        {

        }
    }
}
