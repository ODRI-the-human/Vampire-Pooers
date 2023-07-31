using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public int masterCooldown;
    public AbilityParams[] abilityTypes;
    public int[] coolDowns;
    public int[] charges;
    public int[] attackWeights;
    public bool[] isHoldingAttack; // Specifically for player-controlled characters.
    public bool isAttacking; // Just a single bool, set to true if ANY isHoldingAttack is true.
    public int[] chargeTimers; // Timers for charging attacks. For non-chargeable abilities, this just gets ignored.
    public bool lastAttackCharged; // Used by orbital2
    public float cooldownFac = 1; // For overall cooldown reduction.
    public float[] cooldownFacIndiv; // For individual skills' cooldown reduction.
    [System.NonSerialized] public int noExtraShots = 0;

    public GameObject currentTarget;
    public Vector2 vectorToTarget;
    Vector2 mouseVector;
    Vector3 reticlePos;
    public GameObject cameron;
    public GameObject reticle;
    public GameObject chargeBar;

    public bool isPlayerTeam = true;
    public bool attackAutomatically = false;

    int targetChangeTimer = 0;

    void Start()
    {
        cameron = GameObject.Find("Main Camera");
        if (gameObject.tag == "Player")
        {
            reticle = Instantiate(EntityReferencerGuy.Instance.reticle);
            reticle.transform.SetParent(gameObject.transform);
        }

        if (!isPlayerTeam)
        {
            attackAutomatically = true;
        }

        coolDowns = new int[abilityTypes.Length];
        charges = new int[abilityTypes.Length];
        attackWeights = new int[abilityTypes.Length];
        isHoldingAttack = new bool[abilityTypes.Length];
        chargeTimers = new int[abilityTypes.Length];
        cooldownFacIndiv = new float[abilityTypes.Length];
        for (int i = 0; i < cooldownFacIndiv.Length; i++)
        {
            cooldownFacIndiv[i] = 1;
        }
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            isHoldingAttack[0] = true;
        };

        context.action.canceled += ctx =>
        {
            isHoldingAttack[0] = false;
        };
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            isHoldingAttack[1] = true;
        };

        context.action.canceled += ctx =>
        {
            isHoldingAttack[1] = false;
        };
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            isHoldingAttack[2] = true;
        };

        context.action.canceled += ctx =>
        {
            isHoldingAttack[2] = false;
        };
    }

    public void InputAim(InputAction.CallbackContext context)
    {
        if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "keyboard")
        {
            mouseVector = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            reticlePos = new Vector3(mouseVector.x, mouseVector.y, 0) - cameron.transform.position;
            Vector3 vectorToTarget3 = (new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0) - Camera.main.WorldToScreenPoint(transform.position));
            vectorToTarget = new Vector2(vectorToTarget3.x, vectorToTarget3.y).normalized;
        }
        else
        {
            vectorToTarget = context.ReadValue<Vector2>();
            reticlePos = 5 * new Vector3(vectorToTarget.x, vectorToTarget.y, 0);
        }
    }

    void FindNearestTarget()
    {
        GameObject[] gos;
        if (isPlayerTeam)
        {
            gos = GameObject.FindGameObjectsWithTag("Hostile");
        }
        else
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
        }
        currentTarget = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                currentTarget = go;
                distance = curDistance;
            }
        }
    }

    void Update()
    {
        // For positioning the reticle.
        if (reticle != null)
        {
            Vector3 blobob;

            if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "keyboard")
            {
                blobob = reticlePos + cameron.transform.position;
                //reticle.transform.position = blobob; //cameron.transform.position + reticlePos + new Vector3(0, 0, 5);
            }
            else
            {
                blobob = reticlePos + transform.position;
            }

            reticle.transform.position = new Vector3(blobob.x, blobob.y, -8);
            Color retCol = reticle.GetComponent<SpriteRenderer>().color;
            Vector3 retPosZ0 = new Vector3(blobob.x, blobob.y, 0);
            retCol.a = (retPosZ0 - transform.position).magnitude;
            reticle.GetComponent<SpriteRenderer>().color = retCol;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetChangeTimer--;
        if (targetChangeTimer <= 0 || currentTarget == null)
        {
            targetChangeTimer = 250;
            FindNearestTarget();
        }

        masterCooldown--;
        int attackWeightsSum = 0;

        for (int i = 0; i < abilityTypes.Length; i++) // For resetting cooldowns/availability of abilities.
        {
            coolDowns[i]--;
            if (coolDowns[i] <= 0 && abilityTypes[i] != null && charges[i] <= abilityTypes[i].maxCharges) // Only does the following if this entity doesn't already have the max number of charges of the ability.
            {
                charges[i] = Mathf.Clamp(0, abilityTypes[i].maxCharges, charges[i] + 1);
            }
        }

        if (masterCooldown <= 0 && !attackAutomatically) // For the player's attacks
        {
            for (int i = 0; i < abilityTypes.Length; i++)
            {
                isAttacking = false;

                if (abilityTypes[i].chargeLength == 0 && isHoldingAttack[i] && charges[i] > 0) // For non-charged attacks.
                {
                    UseAttack(abilityTypes[i], i, true, false);
                    isAttacking = true;
                }
                else if (abilityTypes[i].chargeLength > 0 && isHoldingAttack[i] && charges[i] > 0) // Setting timers for charged attacks.
                {
                    if (chargeBar == null)
                    {
                        chargeBar = Instantiate(EntityReferencerGuy.Instance.chargeBar);
                        chargeBar.GetComponent<chargeBarAmt>().owner = gameObject;
                        chargeBar.GetComponent<chargeBarAmt>().indexToTrack = i;
                        chargeBar.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
                        chargeBar.transform.localScale = new Vector3(1, 1, 1);
                    }
                    chargeTimers[i]++;
                    isAttacking = true;
                }

                if (abilityTypes[i].chargeLength != 0 && !isHoldingAttack[i] && charges[i] > 0) // For charged attacks.
                {
                    if (chargeTimers[i] > abilityTypes[i].chargeLength * cooldownFac * cooldownFacIndiv[i])
                    {
                        UseAttack(abilityTypes[i], i, true, true);
                        Destroy(chargeBar);
                    }
                    else if (chargeTimers[i] > 0)
                    {
                        UseAttack(abilityTypes[i], i, true, false);
                        Destroy(chargeBar);
                        
                    }
                }
            }
        }

        if (masterCooldown <= 0 && attackAutomatically) // Only does the following if the master cooldown isn't on and if this object attacks automatically.
        {
            for (int i = 0; i < abilityTypes.Length; i++) // For resetting cooldowns/availability of abilities.
            {
                attackWeights[i] = 0;

                if (abilityTypes[i] != null && masterCooldown <= 0 && charges[i] > 0 && abilityTypes[i].CheckUsability(gameObject, currentTarget)) // If the attack is available, add it to the pool of possible attacks.
                {
                    attackWeights[i] = abilityTypes[i].weight;
                    attackWeightsSum += abilityTypes[i].weight;
                }
            }

            int attackChosen = UnityEngine.Random.Range(0, attackWeightsSum + 1);
            int cumSumWeights = 0;

            for (int i = 0; i < abilityTypes.Length; i++) // Runs through all attacks, picks the one that was chosen by attackChosen.
            {
                cumSumWeights += attackWeights[i];
                if (cumSumWeights > attackChosen)
                {
                    UseAttack(abilityTypes[i], i, false, false);
                    break;
                }
            }
        }
    }

    void OnUseAbility(int index)
    {
        //do nothing nerd
    }
    
    void OnShootEffects()
    {
        //do nothing nerd
    }

    public void UseAttack(AbilityParams abilityToUse, int abilityIndex, bool isPlayer, bool isCharged)
    {
        // Here goes any stuff like moreshot and the like that augment how attacks are used but aren't intrinsic to the specific attack type.

        if (!isPlayer)
        {
            Vector3 vecToTarget3 = currentTarget.transform.position - transform.position;
            vectorToTarget = new Vector2(vecToTarget3.x, vecToTarget3.y).normalized;
        }

        for (int i = 0; i < noExtraShots + 1; i++)
        {
            float currentAngle = 0.9f * (-noExtraShots * 0.5f + i);
            Vector2 vecToUse = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
            abilityToUse.UseAttack(gameObject, currentTarget, vecToUse, isPlayerTeam, abilityIndex, isCharged);
        }
        SendMessage("OnUseAbility", abilityIndex);
        lastAttackCharged = isCharged;

        if (abilityIndex == 0)
        {
            SendMessage("OnShootEffects");
        }
    }
}
