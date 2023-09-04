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
    public Vector3 reticlePos;
    public GameObject cameron;
    public GameObject reticle;
    public GameObject chargeBar;

    public bool isPlayerTeam = true;
    public bool isPlayer = false; // Mainly for setting screen shake, so only player characters can apply the shake.
    public bool attackAutomatically = false;

    int targetChangeTimer = 0;

    void Start()
    {
        FindNearestTarget();
        cameron = GameObject.Find("Main Camera");
        if (gameObject.tag == "Player")
        {
            reticle = Instantiate(EntityReferencerGuy.Instance.reticle);
            reticle.transform.SetParent(gameObject.transform);
        }

        //if (!isPlayerTeam)
        //{
        //    attackAutomatically = true;
        //}

        coolDowns = new int[abilityTypes.Length];
        charges = new int[abilityTypes.Length];
        attackWeights = new int[abilityTypes.Length];
        isHoldingAttack = new bool[abilityTypes.Length];
        chargeTimers = new int[abilityTypes.Length];
        cooldownFacIndiv = new float[abilityTypes.Length];
        for (int i = 0; i < cooldownFacIndiv.Length; i++)
        {
            cooldownFacIndiv[i] = 1;
            charges[i] = abilityTypes[i].maxCharges;
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
            if (coolDowns[i] <= 0 && abilityTypes[i] != null && charges[i] < abilityTypes[i].maxCharges) // Only does the following if this entity doesn't already have the max number of charges of the ability.
            {
                charges[i] = Mathf.Clamp(charges[i] + 1, 0, abilityTypes[i].maxCharges);

                if (abilityTypes[i].rechargeAllShotsAfterCooldown)
                {
                    charges[i] = abilityTypes[i].maxCharges;
                }

                if (charges[i] != abilityTypes[i].maxCharges)
                {
                    coolDowns[i] = Mathf.RoundToInt(abilityTypes[i].coolDownTime * cooldownFacIndiv[i] * cooldownFac);
                }

                if (!isPlayerTeam) // For adding extra to the cooldown timer based on stopwatch shenanigans if this is an enemy
                {
                    coolDowns[i] = Mathf.RoundToInt(coolDowns[i] * EntityReferencerGuy.Instance.stopWatchDebuffAmt);
                }
            }
        }

        if (masterCooldown <= 0 && !attackAutomatically) // For the player's attacks
        {
            isAttacking = false;
            for (int i = 0; i < abilityTypes.Length; i++)
            {
                switch (abilityTypes[i].attackMode)
                {
                    case 0: // automatic abilities (don't need to release firing to fire again)
                        if (isHoldingAttack[i] && charges[i] > 0) // For non-charged attacks.
                        {
                            UseAttack(abilityTypes[i], i, true, false, false, true);
                            isAttacking = true;
                        }
                        break;
                    case 1: // non-automatic abilities like pistol and shotguns and such
                        if (isHoldingAttack[i] && charges[i] > 0) // For non-charged attacks.
                        {
                            UseAttack(abilityTypes[i], i, true, false, false, true);
                            isHoldingAttack[i] = false;
                        }
                        break;
                    case 2: // chargeable attacks
                        if (abilityTypes[i].chargeLength > 0 && isHoldingAttack[i] && charges[i] > 0) // Setting timers for charged attacks.
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
                        if (!isHoldingAttack[i] && charges[i] > 0) // Activating the attack.
                        {
                            if (chargeTimers[i] > abilityTypes[i].chargeLength * cooldownFac * cooldownFacIndiv[i])
                            {
                                UseAttack(abilityTypes[i], i, true, true, false, true);
                                Destroy(chargeBar);
                                chargeTimers[i] = 0;
                            }
                            else if (chargeTimers[i] > 0)
                            {
                                UseAttack(abilityTypes[i], i, true, false, false, true);
                                Destroy(chargeBar);
                                chargeTimers[i] = 0;

                            }
                        }
                        break;
                    case 3: // attacks that need to charge to use, like minigun.
                        if (isHoldingAttack[i] && charges[i] > 0)
                        {
                            isAttacking = true;
                            if (chargeTimers[i] < abilityTypes[i].chargeUpTime * GetCoolDownFac(i)) // Setting timers for charged attacks.
                            {
                                chargeTimers[i]++;
                            }
                            else
                            {
                                UseAttack(abilityTypes[i], i, true, false, false, true);
                            }
                        }
                        else
                        {
                            chargeTimers[i] = 0;
                        }
                        break;
                }
                //    if (abilityTypes[i].chargeLength == 0 && isHoldingAttack[i] && charges[i] > 0) // For non-charged attacks.
                //    {
                //        UseAttack(abilityTypes[i], i, true, false, false, true);
                //        isAttacking = true;
                //    }
                //    else if (abilityTypes[i].chargeLength > 0 && isHoldingAttack[i] && charges[i] > 0) // Setting timers for charged attacks.
                //    {
                //        if (chargeBar == null)
                //        {
                //            chargeBar = Instantiate(EntityReferencerGuy.Instance.chargeBar);
                //            chargeBar.GetComponent<chargeBarAmt>().owner = gameObject;
                //            chargeBar.GetComponent<chargeBarAmt>().indexToTrack = i;
                //            chargeBar.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
                //            chargeBar.transform.localScale = new Vector3(1, 1, 1);
                //        }
                //        chargeTimers[i]++;
                //        isAttacking = true;
                //    }

                //    if (abilityTypes[i].chargeLength != 0 && !isHoldingAttack[i] && charges[i] > 0) // For charged attacks.
                //    {
                //        if (chargeTimers[i] > abilityTypes[i].chargeLength * cooldownFac * cooldownFacIndiv[i])
                //        {
                //            UseAttack(abilityTypes[i], i, true, true, false, true);
                //            Destroy(chargeBar);
                //        }
                //        else if (chargeTimers[i] > 0)
                //        {
                //            UseAttack(abilityTypes[i], i, true, false, false, true);
                //            Destroy(chargeBar);

                //        }
                //    }
                //}
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
                    UseAttack(abilityTypes[i], i, false, false, false, true);
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

    public void UseAttack(AbilityParams abilityToUse, int abilityIndex, bool isPlayer, bool isCharged, bool overrideCooldownSetting, bool playSound)
    {
        // Here goes any stuff like moreshot and the like that augment how attacks are used but aren't intrinsic to the specific attack type.

        if (!isPlayer)
        {
            Vector3 vecToTarget3 = currentTarget.transform.position - transform.position;
            vectorToTarget = new Vector2(vecToTarget3.x, vecToTarget3.y).normalized;
        }

        if (!overrideCooldownSetting)
        {
            charges[abilityIndex]--;
        }

        for (int i = 0; i < noExtraShots + 1; i++)
        {
            if (i != 0) // Just so it only plays the shot sound on the first attack of the iteration.
            {
                playSound = true;
            }

            StartCoroutine(SpawnAttack(abilityToUse, abilityIndex, isPlayer, isCharged, overrideCooldownSetting, playSound, i));
        }
        gameObject.GetComponent<ItemHolder2>().OnAbilityUses(abilityIndex);
        lastAttackCharged = isCharged;

        if (abilityIndex == 0)
        {
            gameObject.GetComponent<ItemHolder2>().OnPrimaryUses();
        }
    }

    public float GetCoolDownFac(int abilityIndex)
    {
        float amount = cooldownFacIndiv[abilityIndex] * cooldownFac;
        if (!isPlayerTeam) // For adding extra to the cooldown timer based on stopwatch shenanigans if this is an enemy. the 1.5x is just so their cooldowns are 50% longer.
        {
            amount *= 1.5f * EntityReferencerGuy.Instance.stopWatchDebuffAmt;
        }
        return amount;
    }

    IEnumerator SpawnAttack(AbilityParams abilityToUse, int abilityIndex, bool isPlayer, bool isCharged, bool overrideCooldownSetting, bool playSound, int i)
    {
        if (!overrideCooldownSetting)
        {
            coolDowns[abilityIndex] = Mathf.RoundToInt(abilityToUse.coolDownTime * GetCoolDownFac(abilityIndex));
            masterCooldown = Mathf.RoundToInt(abilityToUse.masterCooldownTime * GetCoolDownFac(abilityIndex));
        }
        float additionalMult = 1f; // To add to the delay if this is an enemy and stopwatches are in use
        if (!isPlayerTeam)
        {
            additionalMult = EntityReferencerGuy.Instance.stopWatchDebuffAmt;
        }

        yield return new WaitForSecondsRealtime(additionalMult * abilityToUse.spawnDelay * cooldownFacIndiv[abilityIndex] * cooldownFac);

        float currentAngle = (Mathf.PI / 6) * (-noExtraShots * 0.5f + i);
        Vector2 vecToUse = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
        abilityToUse.UseAttack(gameObject, currentTarget, transform.position, vecToUse, isPlayerTeam, abilityIndex, isCharged, overrideCooldownSetting, playSound, false);
    }
}
