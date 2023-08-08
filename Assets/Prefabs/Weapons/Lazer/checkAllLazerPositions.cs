using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAllLazerPositions : MonoBehaviour
{
    public List<GameObject> ignoredHomings = new List<GameObject>();
    public List<GameObject> ignoredHits = new List<GameObject>();

    public AudioClip[] sounds;

    [System.NonSerialized] public float delay = 0.02f;
    public Vector3 vecToMove;
    public GameObject owner;
    public LineRenderer line;
    public GameObject hitParticles;
    public GameObject light;
    public float lineRandPos = 0.2f;
    bool slimLine = false;

    int contacts = 0;

    public Material shoot;
    public Material invis;
    public Material warn;

    GameObject thinguy;
    public GameObject master;
    public bool setVecToMoveAutomatically = true;
    //public bool actuallyHit = true;

    Vector3 originalPosition;
    Vector3 originalDirection;

    bool previousMoveToPlayer;
    bool previousRecieveKnockBack;

    public int attackMode = 0; // As both lazer and crossbow use this script (since they're fairly similar), this differentiates between them. 0 for lazer, 1 for crossbow.
    int numItersSoFar = 0; // Counts up over time, used by crossbow.

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        originalPosition = transform.position;
        originalDirection = new Vector3(transform.up.x, transform.up.y, 0).normalized;
        vecToMove = originalDirection;


        if (owner.GetComponent<NewPlayerMovement>() != null)
        {
            previousMoveToPlayer = owner.GetComponent<NewPlayerMovement>().moveTowardsPlayer;
            previousRecieveKnockBack = owner.GetComponent<NewPlayerMovement>().recievesKnockback;

        }

        thinguy = gameObject;
        if (owner.GetComponent<Attack>().isPlayerTeam)
        {
            thinguy = gameObject;
            StartCoroutine(LateStart(delay, true));
            slimLine = true;
        }
        else
        {
            StartCoroutine(LateStart(delay, false));
            StartCoroutine(LateStart(delay + 0.75f, true));
        }

        if (attackMode == 1)
        {
            Invoke(nameof(DIE), 0.25f);
            line.material = shoot;
            line.widthMultiplier *= 0.5f;
        }
    }

    void FixedUpdate()
    {
        if (attackMode == 1)
        {
            if (vecToMove != Vector3.zero)
            {
                line.positionCount = numItersSoFar + 7;
                DoMotion(true, numItersSoFar, 7); // The higher numIterations with, the faster the projectile will move.
                numItersSoFar += 7;
            }
            else
            {
                Destroy(gameObject.GetComponent<MeshFilter>());
                Destroy(gameObject.GetComponent<MeshRenderer>());
            }
        }
    }

    void DoMotion(bool actuallyHit, int startingIterations, int numIterations)
    {
        int mode = 0; // A mode to determine the current behaviour of the lazer (0 for player shot, 1 for enemy warn, 2 for enemy shot)
        if (!owner.GetComponent<Attack>().isPlayerTeam)
        {
            if (actuallyHit)
            {
                mode = 2;
            }
            else
            {
                mode = 1;
            }
        }

        if (owner.GetComponent<Attack>().isPlayerTeam)
        {
            if (attackMode == 0)
            {
                mode = 0;
            }
            else
            {
                mode = 1;
            }
        }

        for (int i = startingIterations; i < startingIterations + numIterations; i++)
        {
            //Debug.Log(vecToMove.ToString());
            transform.rotation = Quaternion.LookRotation(vecToMove) * Quaternion.Euler(0, 90, 0);
            if (i + 1 >= line.positionCount && mode == 2) // Breaking out of loop if the enemy's actual shot is at its final iteration.
            {
                break;
            }

            switch (mode)
            {
                case 0:
                    line.SetPosition(i, new Vector3(transform.position.x, transform.position.y, -1) + new Vector3(Random.Range(-lineRandPos, lineRandPos), Random.Range(-lineRandPos, lineRandPos), 0));
                    transform.position += 0.6f * vecToMove;
                    break;
                case 1:
                    line.SetPosition(i, new Vector3(transform.position.x, transform.position.y, -1));
                    transform.position += 0.6f * vecToMove;
                    break;
                case 2:
                    line.SetPosition(i, new Vector3(transform.position.x, transform.position.y, -1) + new Vector3(Random.Range(-lineRandPos, lineRandPos), Random.Range(-lineRandPos, lineRandPos), 0));
                    transform.position = line.GetPosition(i + 1) + new Vector3(0, 0, 1);
                    break;
            }

            // ending the loop if the lazer is not moving.
            if (vecToMove == Vector3.zero && (mode == 0 || mode == 1))
            {
                line.positionCount = i;
                break;
            }

            // Applying homing effect.
            if (gameObject.GetComponent<ItemHOMING>() != null)
            {
                //Debug.Log("pdaspodfsj");
                Collider2D[] shitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3 * gameObject.GetComponent<ItemHOMING>().instances);
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
                            vecToMove = (vecToMove + 0.3f * gameObject.GetComponent<ItemHOMING>().instances * vectorToEnemy.normalized / (1.1f + Mathf.Pow(vectorToEnemy.magnitude, 1.2f))).normalized;

                            Vector3 colVec = col.transform.position - transform.position;
                            colVec = new Vector3(colVec.x, colVec.y, 0);

                            //Debug.Log(colVec.magnitude.ToString());
                            if (colVec.magnitude < 0.5f)
                            {
                                ignoredHomings.Add(col.gameObject);
                            }
                        }

                        break;
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

                    if (doContinue && actuallyHit)
                    {
                        //Debug.Log("fsdmmfds");
                        //float procMoment = 100f - 100f * gameObject.GetComponent<DealDamage>().critProb * gameObject.GetComponent<DealDamage>().procCoeff;
                        //float pringle = Random.Range(0f, 100f);
                        //float critMult = 1;
                        //bool isCrit = false;
                        //if (pringle > procMoment)
                        //{
                        //    critMult = gameObject.GetComponent<DealDamage>().critMult;
                        //    //Instantiate(CritAudio);
                        //    isCrit = true;
                        //}
                        gameObject.GetComponent<DealDamage>().CalculateDamage(col.gameObject, gameObject);
                        //float damageAmount = gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
                        //col.gameObject.GetComponent<HPDamageDie>().Hurty(gameObject.GetComponent<DealDamage>().damageAmt, isCrit, true, 1, (int)DAMAGETYPES.ELECTRIC, false, gameObject);
                        //gameObject.GetComponent<ParticleSystem>().Emit(50);
                        //gameObject.GetComponent<DealDamage>().TriggerTheOnHits(col.gameObject);
                        ignoredHits.Add(col.gameObject);
                        if (attackMode == 1)
                        {
                            col.gameObject.GetComponent<NewPlayerMovement>().knockBackVector = vecToMove * 25f;
                        }
                    }

                    if (doContinue && gameObject.GetComponent<ItemPIERCING>() != null)
                    {
                        gameObject.GetComponent<DealDamage>().damageAmt *= 1 + 0.2f * gameObject.GetComponent<ItemPIERCING>().instances;
                        gameObject.GetComponent<DealDamage>().finalDamageStat = gameObject.GetComponent<DealDamage>().damageAmt;
                        ignoredHits.Add(col.gameObject);
                    }
                }

                if ((col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet"))
                {
                    // Damaging homing mines.
                    bool doContinue = true;

                    foreach (GameObject thingy in ignoredHits)
                    {
                        if (col.gameObject == thingy)
                        {
                            doContinue = false;
                        }
                    }

                    if (doContinue && actuallyHit && col.gameObject.GetComponent<HPDamageDie>() != null)
                    {
                        //Debug.Log("fsdmmfds");
                        //float procMoment = 100f - 100f * gameObject.GetComponent<DealDamage>().critProb * gameObject.GetComponent<DealDamage>().procCoeff;
                        //float pringle = Random.Range(0f, 100f);
                        //float critMult = 1;
                        //bool isCrit = false;
                        //if (pringle > procMoment)
                        //{
                        //    critMult = gameObject.GetComponent<DealDamage>().critMult;
                        //    //Instantiate(CritAudio);
                        //    isCrit = true;
                        //}
                        gameObject.GetComponent<DealDamage>().CalculateDamage(col.gameObject, gameObject);
                        //float damageAmount = gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
                        //col.gameObject.GetComponent<HPDamageDie>().Hurty(gameObject.GetComponent<DealDamage>().damageAmt, isCrit, true, 1, (int)DAMAGETYPES.ELECTRIC, false, gameObject);
                        //gameObject.GetComponent<ParticleSystem>().Emit(50);
                        //gameObject.GetComponent<DealDamage>().TriggerTheOnHits(col.gameObject);
                        ignoredHits.Add(col.gameObject);
                    }

                    // Applying contact shots.
                    if (gameObject.GetComponent<ItemCONTACT>() != null && contacts != gameObject.GetComponent<ItemCONTACT>().instances * 2 && actuallyHit)
                    {
                        int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
                        int LayerEnemyBullet = LayerMask.NameToLayer("EnemyBullets");

                        if (gameObject.tag == "PlayerBullet")
                        {
                            col.gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.playerBulletMaterial;
                            col.gameObject.layer = LayerPlayerBullet;
                        }
                        else
                        {
                            col.gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.enemyBulletMaterial;
                            col.gameObject.layer = LayerEnemyBullet;
                        }

                        col.gameObject.tag = gameObject.tag;
                        contacts++;
                        float velMag = (col.gameObject.GetComponent<Rigidbody2D>().velocity).magnitude;
                        Vector3 vel = (col.gameObject.GetComponent<Rigidbody2D>().velocity).normalized;
                        Vector3 blinkus = vecToMove.normalized;
                        col.gameObject.GetComponent<Rigidbody2D>().velocity = velMag * (2 * blinkus + vel).normalized;
                    }

                    // Bouncing lazers off contact shots, dodge explosions and the like.
                    if (col.gameObject.GetComponent<dieOnContactWithBullet>() != null && actuallyHit)
                    {
                        if (gameObject.tag == "enemyBullet" && col.gameObject.tag == "PlayerBullet")
                        {
                            gameObject.tag = "PlayerBullet";
                        }

                        if (gameObject.tag == "PlayerBullet" && col.gameObject.tag == "enemyBullet")
                        {
                            gameObject.tag = "enemyBullet";
                        }

                        vecToMove = transform.position - col.transform.position;
                        vecToMove = new Vector3(vecToMove.x, vecToMove.y, 0).normalized;
                        col.gameObject.GetComponent<dieOnContactWithBullet>().instances -= 1;
                    }
                }

                if (col.gameObject.tag == "Wall")
                {
                    if (actuallyHit)
                    {
                        //GameObject particles = Instantiate(hitParticles, transform.position, Quaternion.Euler(0, 0, 0));
                        //var renderer = particles.GetComponent<ParticleSystemRenderer>();
                        //renderer.trailMaterial = shoot; // Applies the new value directly to the Particle System
                        //SendMessage("OnWallHit");
                        gameObject.GetComponent<DealDamage>().CalculateDamage(col.gameObject, gameObject);
                    }

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
    }

    IEnumerator LateStart(float waitTime, bool actuallyHit)
    {
        yield return new WaitForSeconds(waitTime);
        transform.localScale = new Vector3(1, 1, 1);

        if (setVecToMoveAutomatically)
        {
            vecToMove = originalDirection;
        }

        if (actuallyHit)
        {
            SoundManager.Instance.PlaySound(sounds[Random.Range(0, sounds.Length)]);
        }

        if (attackMode == 0)
        {
            transform.position = originalPosition;
            if (!actuallyHit) // only do this on the warn shot.
            {
                line.positionCount = 150;
            }
            DoMotion(actuallyHit, 0, 150);

            if (actuallyHit)
            {
                Invoke(nameof(DIE), 0.25f);
                line.material = shoot;
                line.widthMultiplier = (gameObject.GetComponent<DealDamage>().finalDamageStat / 2 + 25) / 100;
                slimLine = true;
                owner.GetComponent<NewPlayerMovement>().moveTowardsPlayer = previousMoveToPlayer;
                owner.GetComponent<NewPlayerMovement>().recievesKnockback = previousRecieveKnockBack;
            }
            else
            {
                line.material = warn;
                line.widthMultiplier = 0.1f;
                owner.GetComponent<NewPlayerMovement>().moveTowardsPlayer = false;
                owner.GetComponent<NewPlayerMovement>().recievesKnockback = false;
            }
        }
    }

    void Update()
    {
        if (slimLine)
        {
            line.widthMultiplier /= 1 + 8 * Time.deltaTime;
        }

        if (owner == null)
        {
            Destroy(gameObject);
        }
    }

    void DIE()
    {
        Destroy(gameObject);
    }
}
