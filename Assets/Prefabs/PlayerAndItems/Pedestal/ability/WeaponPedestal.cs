using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPedestal : MonoBehaviour
{
    public AbilityParams[] primaryAbilities;
    public AbilityParams[] secondaryAbilities;
    public AbilityParams[] paramsToUse;
    public bool isPrimary = true; // true if pedestal will pick a primary, false if the pedestal will pick a secondary.
    public bool overrideRandomSelection = false; // if a source will drop a particular ability, set this to false.
    public AbilityParams chosenAbility;
    public int abilityWeightsSum;

    // Start is called before the first frame update
    void Start()
    {
        if (!overrideRandomSelection) // If the pedestal is to select a random weapon, it does the following
        {
            if (Random.value > 0.5f)
            {
                isPrimary = true;
                paramsToUse = primaryAbilities;
            }
            else
            {
                isPrimary = false;
                paramsToUse = secondaryAbilities;
            }

            abilityWeightsSum = 0;
            foreach (AbilityParams ability in paramsToUse)
            {
                abilityWeightsSum += ability.dropWeight;
            }

            int randomWacky = Random.Range(0, abilityWeightsSum);
            //Debug.Log(randomWacky.ToString());
            int currentWeightSum = 0;
            for (int i = 0; i < paramsToUse.Length; i++)
            {
                currentWeightSum += paramsToUse[i].dropWeight;
                if (randomWacky <= currentWeightSum)
                {
                    chosenAbility = paramsToUse[i];
                    break;
                }
            }
        }

        if (chosenAbility.weaponMesh == null)
        {
            gameObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.defaultWeaponMesh;
        }
        else
        {
            gameObject.GetComponent<MeshFilter>().mesh = chosenAbility.weaponMesh;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 45f * Time.time);
    }

    public void StartPickup(GameObject pickerUpper)
    {
        pickerUpper.GetComponent<Attack>().abilityTypes[0] = chosenAbility;
        pickerUpper.GetComponent<Attack>().charges[0] = chosenAbility.maxCharges;
        pickerUpper.GetComponent<Attack>().coolDowns[0] = 0;
        Destroy(gameObject);
    }
}
