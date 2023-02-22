using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAllLazerPositions : MonoBehaviour
{
    public List<Vector3> hitboxPosses = new List<Vector3>();
    public List<GameObject> ignoredHomings = new List<GameObject>();
    public List<GameObject> ignoredPiercings = new List<GameObject>();
    public List<GameObject> ignoredHits = new List<GameObject>();
    public List<GameObject> ignoredSplits = new List<GameObject>();

    public Vector3 vecToMove;
    public GameObject owner;
    public LineRenderer line;

    public GameObject master;

    int contacts = 0;

    public Material shoot;
    public Material invis;

    GameObject thinguy;
    public bool setVecToMoveAutomatically = true;

    // Start is called before the first frame update
    void Start()
    {
        thinguy = gameObject;
        StartCoroutine(LateStart(0.02f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (setVecToMoveAutomatically)
        {
            vecToMove = new Vector3(transform.up.x, transform.up.y, 0).normalized;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        line.positionCount = 150;

        Invoke(nameof(DIE), 0.25f);

        for (int i = 0; i < 150; i++)
        {
            //Debug.Log(vecToMove.ToString());

            if (vecToMove == Vector3.zero)
            {
                line.positionCount = i;
                break;
            }

            line.SetPosition(i, new Vector3(transform.position.x, transform.position.y, -2));
            hitboxPosses.Add(transform.position);

            transform.position += 0.4f * vecToMove;

            // Applying homing effect.
            if (gameObject.GetComponent<ItemHOMING>() != null)
            {
                //Debug.Log("pdaspodfsj");
                Collider2D[] shitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3.5f * gameObject.GetComponent<ItemHOMING>().instances);
                foreach (var col in shitColliders)
                {
                    //Debug.Log("pdaspodfsj");
                    if ((col.gameObject.tag == "Player" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "Hostile" && gameObject.tag == "PlayerBullet"))//(col.gameObject.tag == "Hostile" && col.gameObject.tag == "PlayerBullet") || (col.gameObject.tag == "Player" && col.gameObject.tag == "enemyBullet"))
                    {
                        bool doContinue = true;

                        foreach (GameObject thingy in ignoredHomings)
                        {
                            if (col.gameObject == thingy)
                            {
                                doContinue = false;
                            }
                        }

                        if (doContinue)
                        {
                            Vector3 vectorToEnemy = col.gameObject.transform.position - transform.position;
                            vectorToEnemy = new Vector3(vectorToEnemy.x, vectorToEnemy.y, 0);
                            vecToMove = (vecToMove + 0.35f * vectorToEnemy.normalized).normalized;

                            Vector3 colVec = col.transform.position - transform.position;
                            colVec = new Vector3(colVec.x, colVec.y, 0);

                            //Debug.Log(colVec.magnitude.ToString());
                            if (colVec.magnitude < 0.5f)
                            {
                                ignoredHomings.Add(col.gameObject);
                            }
                        }
                    }
                }
            }

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.12f);
            foreach (var col in hitColliders)
            {

                if ((col.gameObject.tag == "Player" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "Hostile" && gameObject.tag == "PlayerBullet"))
                {
                    // Hurting any player/enemy along the way.
                    bool doContinue = true;

                    foreach (GameObject thingy in ignoredHits)
                    {
                        if (col.gameObject == thingy)
                        {
                            doContinue = false;
                        }
                    }

                    if (doContinue)
                    {
                        //Debug.Log("fsdmmfds");
                        float procMoment = 100f - 100f * gameObject.GetComponent<DealDamage>().critProb * gameObject.GetComponent<DealDamage>().procCoeff;
                        float pringle = Random.Range(0f, 100f);
                        float critMult = 1;
                        bool isCrit = false;
                        if (pringle > procMoment)
                        {
                            critMult = gameObject.GetComponent<DealDamage>().critMult;
                            //Instantiate(CritAudio);
                            isCrit = true;
                        }
                        float damageAmount = gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
                        col.gameObject.GetComponent<HPDamageDie>().Hurty(damageAmount, isCrit, true, 1);
                        gameObject.GetComponent<DealDamage>().TriggerTheOnHits(col.gameObject);
                        ignoredHits.Add(col.gameObject);
                    }

                    // Applying piercing.
                    bool doTheseEffects = true;

                    foreach (GameObject blimpy in ignoredPiercings)
                    {
                        if (col.gameObject == blimpy)
                        {
                            doTheseEffects = false;
                        }
                    }

                    if (doTheseEffects && gameObject.GetComponent<ItemPIERCING>() != null)
                    {
                        gameObject.GetComponent<DealDamage>().finalDamageMult *= 1 + 0.2f * gameObject.GetComponent<ItemPIERCING>().instances;
                        ignoredPiercings.Add(col.gameObject);
                    }

                    // Applying split shots.
                    if (doTheseEffects && gameObject.GetComponent<ItemSPLIT>() != null && gameObject.GetComponent<ItemSPLIT>().canSplit)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            GameObject merman = Instantiate(thinguy, transform.position, Quaternion.Euler(0, 0, 0));
                            merman.GetComponent<checkAllLazerPositions>().vecToMove = (2 * j - 1) * new Vector3(vecToMove.y, -vecToMove.x, 0); // Rotates vector 90 degrees.
                            merman.GetComponent<checkAllLazerPositions>().setVecToMoveAutomatically = false;
                            merman.GetComponent<DealDamage>().finalDamageMult = 0.3f * gameObject.GetComponent<ItemSPLIT>().instances * gameObject.GetComponent<DealDamage>().finalDamageMult;
                            merman.GetComponent<ItemSPLIT>().canSplit = false;
                            ignoredSplits.Add(col.gameObject);
                        }
                    }
                }

                if ((col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet"))
                {
                    // Damaging homing mines.
                    if (col.gameObject.GetComponent<HPDamageDie>() != null)
                    {
                        float procMoment = 100f - 100f * gameObject.GetComponent<DealDamage>().critProb * gameObject.GetComponent<DealDamage>().procCoeff;
                        float pringle = Random.Range(0f, 100f);
                        float critMult = 1;
                        bool isCrit = false;
                        if (pringle > procMoment)
                        {
                            critMult = gameObject.GetComponent<DealDamage>().critMult;
                            //Instantiate(CritAudio);
                            isCrit = true;
                        }
                        float damageAmount = gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
                        col.gameObject.GetComponent<HPDamageDie>().Hurty(damageAmount, isCrit, true, 1);
                        ignoredHits.Add(col.gameObject);
                    }

                    // Applying contact shots.
                    if (gameObject.GetComponent<ItemCONTACT>() != null && contacts != gameObject.GetComponent<ItemCONTACT>().instances * 2)
                    {
                        int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
                        int LayerEnemyBullet = LayerMask.NameToLayer("EnemyBullets");

                        if (gameObject.tag == "PlayerBullet")
                        {
                            col.gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().playerBulletMaterial;
                            col.gameObject.layer = LayerPlayerBullet;
                        }
                        else
                        {
                            col.gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().enemyBulletMaterial;
                            col.gameObject.layer = LayerEnemyBullet;
                        }

                        col.gameObject.tag = gameObject.tag;
                        contacts++;
                        float velMag = (col.gameObject.GetComponent<Rigidbody2D>().velocity).magnitude;
                        Vector3 vel = (col.gameObject.GetComponent<Rigidbody2D>().velocity).normalized;
                        Vector3 blinkus = vecToMove.normalized;
                        col.gameObject.GetComponent<Rigidbody2D>().velocity = velMag * (2 * blinkus + vel).normalized;
                    }
                }

                if (col.gameObject.tag == "Wall")
                {
                    // Damaging rocks.
                    col.gameObject.GetComponent<obstHP>().owMyEntireRockIsInPain(gameObject);

                    // Applying bouncy effect.
                    if (gameObject.GetComponent<ItemBOUNCY>() != null && gameObject.GetComponent<ItemBOUNCY>().bouncesLeft != 0)
                    {
                        //Debug.Log("lazer collided.");
                        Vector3 enemyPos = col.gameObject.transform.position;
                        Vector3 bulletPos = transform.position;

                        Vector3 colVector = (bulletPos - enemyPos).normalized;

                        if (Mathf.Abs(colVector.y) > Mathf.Abs(colVector.x))
                        {
                            colVector.x = vecToMove.x;
                        }
                        else
                        {
                            colVector.y = vecToMove.y;
                        }

                        vecToMove = new Vector3(colVector.x, colVector.y, 0).normalized;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameObject.GetComponent<ItemBOUNCY>().bouncesLeft--;
                        ignoredHits.Clear();
                        break;
                    }
                    else
                    {
                        vecToMove = Vector3.zero;
                    }
                }
            }
        }

        line.material = shoot;
        line.widthMultiplier = (gameObject.GetComponent<DealDamage>().finalDamageStat / 2 + 25) / 100;
    }

    void Update()
    {
        line.widthMultiplier /= 1 + 8 * Time.deltaTime;
    }

    void DIE()
    {
        Destroy(gameObject);
    }
}
