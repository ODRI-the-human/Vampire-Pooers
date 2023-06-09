using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceInFunnyDirection : MonoBehaviour
{
    float dirFacing = 1;
    float prevDirFacing = 1;
    public float dirChangeTimer = -20;
    public float dirChangeTimerMax;

    public GameObject owner;
    public GameObject camera;
    float speed = 1;
    bool keepCounting = false;
    GameObject master;

    void Start()
    {
        owner = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }

        transform.position = owner.transform.position;

        Vector3 blimpPos = owner.GetComponent<Attack>().vectorToTarget;
        transform.rotation = Quaternion.LookRotation(blimpPos, new Vector3(0, 0, 1));
        transform.Rotate(0, 0, 90 + 90 * dirFacing, Space.World);
        gameObject.GetComponent<DealDamage>().damageBase = owner.GetComponent<DealDamage>().damageBase;

        {
            dirChangeTimer -= speed * Time.deltaTime;

            if (dirChangeTimer < 0 && keepCounting)
            {
                keepCounting = false;
            }

            //dirChangeTimer = Mathf.Clamp(dirChangeTimer, 0, 1);
            dirFacing = Mathf.Lerp(prevDirFacing, -prevDirFacing, Mathf.Pow(1 - 2 * (dirChangeTimer / dirChangeTimerMax), 1));

        }
    }
    //float dirFacing = 1;
    //float prevDirFacing = 1;
    //public float dirChangeTimer = -20;
    //public float dirChangeTimerMax;

    //public GameObject owner;
    //public GameObject camera;
    //public GameObject hitSFX;
    //float speed = 1;
    //bool keepCounting = false;
    //GameObject master;

    //public GameObject contactMan;

    //float bulletSpeedMult = 15;
    //int numDeflected = 0; // for the 4dirmarty item!

    //// Start is called before the first frame update
    //void Start()
    //{
    //    master = EntityReferencerGuy.Instance.master;
    //    Debug.Log("Bat spawned");
    //    gameObject.GetComponent<DealDamage>().owner = owner;
    //    gameObject.GetComponent<DealDamage>().master = master;
    //    //gameObject.GetComponent<ItemHolder>().isGuy = false;
    //    //gameObject.GetComponent<ItemHolder>().isBullet = true;
    //    camera = GameObject.Find("Main Camera");
    //}

    //public void Attackment(float fireRate)
    //{
    //    dirChangeTimer = Mathf.Clamp(fireRate, 0, 10);
    //    dirChangeTimerMax = dirChangeTimer;
    //    prevDirFacing = dirFacing;
    //    gameObject.GetComponent<Collider2D>().enabled = true;
    //    contactMan.GetComponent<Collider2D>().enabled = true;

    //    gameObject.GetComponent<DealDamage>().finalDamageMult = owner.GetComponent<DealDamage>().finalDamageMult;
    //    gameObject.SendMessage("DetermineShotRolls"); // For items like sawshot and brick.
    //    Debug.Log("hitty");
    //    keepCounting = true;
    //}

    //void Update()
    //{
    //    if (owner == null)
    //    {
    //        Destroy(gameObject);
    //    }

    //    transform.position = owner.transform.position;

    //    Vector3 blimpPos = owner.GetComponent<Attack>().vectorToTarget;
    //    transform.rotation = Quaternion.LookRotation(blimpPos, new Vector3(0, 0, 1));
    //    transform.Rotate(0, 0, 90 + 90 * dirFacing, Space.World);
    //    gameObject.GetComponent<DealDamage>().damageBase = owner.GetComponent<DealDamage>().damageBase;
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    dirChangeTimer -= speed;

    //    if (dirChangeTimer < 0 && keepCounting)
    //    {
    //        gameObject.GetComponent<Collider2D>().enabled = false;
    //        contactMan.GetComponent<Collider2D>().enabled = false;
    //        gameObject.SendMessage("EndOfShotRolls"); // removing any effects like that of brick or sawshot.
    //        keepCounting = false;
    //    }

    //    //dirChangeTimer = Mathf.Clamp(dirChangeTimer, 0, 1);
    //    dirFacing = Mathf.Lerp(prevDirFacing, -prevDirFacing, Mathf.Pow(1 - 2 * (dirChangeTimer / dirChangeTimerMax), 1));

    //}

    //public void ApplyContact(GameObject bullet) // Applying the bat's unique contact shots.
    //{
    //    if (bullet.GetComponent<Bullet_Movement>() != null)
    //    {
    //        Vector2 blombo = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    //        blombo = blombo.normalized;
    //        bullet.GetComponent<Rigidbody2D>().velocity = blombo * bulletSpeedMult;
    //        bullet.SendMessage("Undo");
    //        bullet.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
    //        bullet.GetComponent<ItemHolder>().ApplyAll();
    //        bullet.GetComponent<DealDamage>().finalDamageMult = 2 * gameObject.GetComponent<DealDamage>().finalDamageMult;
    //        bullet.GetComponent<DealDamage>().owner = owner;

    //        for (int i = 0; i < owner.GetComponent<Attack>().noExtraShots + 1; i++) // For applying moreShot.
    //        {
    //            GameObject boolet = null;
    //            float currentAngle = 0;

    //            if (i == 0)
    //            {
    //                boolet = bullet;
    //            }
    //            else
    //            {
    //                boolet = Instantiate(bullet);
    //                currentAngle = 0.6f * Random.Range(-1.1f, 1.1f);
    //            }

    //            Vector3 vectorToTarget = bullet.GetComponent<Rigidbody2D>().velocity;
    //            Vector3 newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
    //            boolet.GetComponent<Rigidbody2D>().velocity = newShotVector;
    //        }

    //        numDeflected++;
    //    }

    //    if (numDeflected >= 4 && owner.GetComponent<ItemFOURDIRMARTY>() != null)
    //    {
    //        for (int i = 0; i < 4 * owner.GetComponent<ItemFOURDIRMARTY>().instances; i++) // For applying moreShot.
    //        {
    //            GameObject boolet = Instantiate(bullet);
    //            float currentAngle = i * (Mathf.PI) / (2 * owner.GetComponent<ItemFOURDIRMARTY>().instances);
    //            Vector3 vectorToTarget = bullet.GetComponent<Rigidbody2D>().velocity;
    //            Vector3 newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
    //            boolet.GetComponent<Rigidbody2D>().velocity = newShotVector;
    //        }

    //        numDeflected = 0;
    //    }
    //}

    //public void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Hostile")
    //    {
    //        col.gameObject.AddComponent<hitIfKBVecHigh>();
    //        Instantiate(hitSFX);
    //        master.GetComponent<visualPoopoo>().bigHitFreeze(0.02f);
    //        camera.GetComponent<cameraMovement>().CameraShake(30);
    //    }
    //}
}
