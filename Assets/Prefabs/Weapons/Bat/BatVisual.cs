using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVisual : MonoBehaviour
{
    float dirFacing = 1;
    float prevDirFacing = 1;
    public float dirChangeTimer = -20;
    public float dirChangeTimerMax;

    float fireRate;

    public GameObject owner;
    public GameObject bat;
    public GameObject camera;
    float speed = 1;
    bool keepCounting = false;
    GameObject master;

    void Start()
    {
        bat = Instantiate(EntityReferencerGuy.Instance.bat);
        bat.transform.parent = transform;
        owner = gameObject;
    }

    public void Kill()
    {
        Destroy(this);
        Destroy(bat);
    }

    void Update()
    {
        fireRate = gameObject.GetComponent<Attack>().fireTimerActualLength;

        transform.position = owner.transform.position;

        Vector3 blimpPos = owner.GetComponent<Attack>().vectorToTarget;
        bat.transform.rotation = Quaternion.LookRotation(blimpPos, new Vector3(0, 0, 1));
        bat.transform.Rotate(0, 0, 90 + 90 * dirFacing, Space.World);
        bat.transform.position = transform.position;
        gameObject.GetComponent<DealDamage>().damageBase = owner.GetComponent<DealDamage>().damageBase;

        {
            dirChangeTimer -= fireRate * 10 * Time.deltaTime;

            if (dirChangeTimer < 0 && keepCounting)
            {
                keepCounting = false;
            }

            //dirChangeTimer = Mathf.Clamp(dirChangeTimer, 0, 1);
            dirFacing = Mathf.Lerp(prevDirFacing, -prevDirFacing, Mathf.Pow(1 - 2 * (dirChangeTimer / dirChangeTimerMax), 1));

        }
    }

    void OnShootEffects()
    {
        dirChangeTimer = Mathf.Clamp(fireRate, 0, 10);
        dirChangeTimerMax = dirChangeTimer;
        prevDirFacing = dirFacing;
        keepCounting = true;
    }
}
