using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public List<Vector3> positions = new List<Vector3>();
    public GameObject owner;
    float timer = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = 45 * new Vector3(transform.up.x, transform.up.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Invoke(nameof(DoOnDestroys), 0.4f);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        positions.Add(transform.position);
        timer--;
    }

    public void DoOnDestroys()
    {
        owner.GetComponent<lightningFire>().FuckingKillComputer(positions, timer);
        owner.GetComponent<lightningFire>().hitboxPosses = positions;
        Destroy(gameObject);
    }
}
