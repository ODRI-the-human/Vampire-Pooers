using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBarValue : MonoBehaviour
{
    public float valueFac;
    public GameObject owner;
    public Slider slider;
    public int valueToTrack;

    // The below is only for the HP draining/healing effects! Ignore it for all other types.
    public GameObject sliderObjToCopyPosOf;
    public float lastVarVal;
    [System.NonSerialized] public float decayTimeFac = 50;
    public float lastVal = 0;
    public bool keepCheckingTime = true;

    // Start is called before the first frame update
    void Start()
    {
        owner = gameObject.GetComponentInParent<setUIOwner>().player;

        //if (valueToTrack != 2)
        //{
        //    owner = rootScript.player;
        //}
        //else
        //{
        //    owner = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<setUIOwner>().player;
        //}

        switch (valueToTrack)
        {
            case 1:
                lastVal = owner.GetComponent<HPDamageDie>().HP;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = 0;
        float maxValue = 0;

        switch (valueToTrack)
        {
            case 0: // HP
                value = owner.GetComponent<HPDamageDie>().HP;
                maxValue = owner.GetComponent<HPDamageDie>().MaxHP;
                break;
            case 1: // HP loss
                if (lastVal != owner.GetComponent<HPDamageDie>().HP && keepCheckingTime)
                {
                    keepCheckingTime = false;
                }

                if (lastVal > owner.GetComponent<HPDamageDie>().HP)
                {
                    lastVal -= decayTimeFac * Time.deltaTime;
                    keepCheckingTime = true;
                }

                if (lastVal < owner.GetComponent<HPDamageDie>().HP)
                {
                    lastVal = owner.GetComponent<HPDamageDie>().HP;
                }

                value = lastVal; //(lastVal - owner.GetComponent<HPDamageDie>().HP) * decayTimeFac
                maxValue = owner.GetComponent<HPDamageDie>().MaxHP;
                break;
            case 2: // Healing
                //if (lastVal != owner.GetComponent<HPDamageDie>().HP && keepCheckingTime)
                //{
                //    timeOfChange = Time.time;
                //    keepCheckingTime = false;
                //}
                //if (lastVal < owner.GetComponent<HPDamageDie>().HP)
                //{
                //    lastVal += decayTimeFac * Time.deltaTime;
                //    keepCheckingTime = true;
                //    value = lastVal; //(lastVal - owner.GetComponent<HPDamageDie>().HP) * decayTimeFac
                //}
                //else
                //{
                //    timeOfChange = Time.time;
                //    keepCheckingTime = false;
                //    value = 0;
                //}

                //float missingHealthFac = owner.GetComponent<HPDamageDie>().MaxHP - owner.GetComponent<HPDamageDie>().HP;

                //if (missingHealthFac != lastVal && keepCheckingTime)
                //{
                //    lastVal += missingHealthFac;
                //    keepCheckingTime = false;
                //}

                //if (lastVal > 0)
                //{
                //    lastVal -= decayTimeFac * Time.deltaTime;
                //}
                //else
                //{
                //    lastVal = 0;
                //    keepCheckingTime = true;
                //}

                //value = lastVal;
                //maxValue = owner.GetComponent<HPDamageDie>().MaxHP;

                //Debug.Log("value/maxval: " + value.ToString() + "/" + maxValue.ToString());

                float missingHealth = owner.GetComponent<HPDamageDie>().MaxHP - owner.GetComponent<HPDamageDie>().HP;
                if (missingHealth < lastVarVal) // if the player has more health than the last value of HP (i.e. has healed)
                {
                    // Set the new lastVarVal, and set new valueFac corresponding to the what % of the player's health they gained back:
                    lastVal = lastVarVal - missingHealth;
                    lastVarVal = missingHealth;
                }
                else
                {
                    if (missingHealth > lastVarVal)
                    {
                        lastVal = 0;
                        lastVarVal = missingHealth;
                    }
                }

                if (lastVal > 0)
                {
                    lastVal -= decayTimeFac * Time.deltaTime;
                }

                //Debug.Log("lastVal: " + lastVal.ToString());

                value = lastVal;
                maxValue = owner.GetComponent<HPDamageDie>().MaxHP;
                break;
            case 3: // XP
                int currentLevel = owner.GetComponent<LevelUp>().level;
                int nextLevel = currentLevel + 1;

                float nextXP = owner.GetComponent<LevelUp>().nextXP;
                float floorXP = (currentLevel - 1) * 40 + 10 * Mathf.Pow((currentLevel - 1), 2);
                float XP = owner.GetComponent<LevelUp>().XP;

                value = XP - floorXP;
                maxValue = nextXP - floorXP;
                break;
            case 4:
                //value = owner.GetComponent<secondaryAbility>().abilityOneCooldown;
                //maxValue = owner.GetComponent<secondaryAbility>().abilityOneMaxCooldown;
                break;
            case 5:
                //value = owner.GetComponent<secondaryAbility>().abilityTwoCooldown;
                //maxValue = owner.GetComponent<secondaryAbility>().abilityTwoMaxCooldown;
                break;
        }

        valueFac = value / maxValue;
        SetValue();
    }

    public void SetValue()
    {
        if (!float.IsNaN(valueFac))
        {
            slider.value = valueFac;
        }
        //if (valueToTrack == 2)
        //{
        //    Debug.Log("slider val: " + slider.value.ToString());
        //}
    }
}
