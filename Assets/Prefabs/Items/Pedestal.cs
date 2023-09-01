using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public int weightsSum;
    public bool isItemPedestal;

    // Items
    public ItemSOInst[] items;
    public List<ItemSOInst> qual1Items = new List<ItemSOInst>();
    public List<ItemSOInst> qual2Items = new List<ItemSOInst>();
    public List<ItemSOInst> qual3Items = new List<ItemSOInst>();
    public ItemSOInst chosenItem;
    int[] qualityWeights = new int[] { 100, 35, 5 }; // Grey, green, yellow.

    // Abilities
    public AbilityParams[] primaryAbilities;
    public AbilityParams[] secondaryAbilities;
    public AbilityParams[] paramsToUse;
    public bool isPrimary = true; // true if pedestal will pick a primary, false if the pedestal will pick a secondary.
    public bool overrideRandomSelection = false; // if a source will drop a particular ability, set this to false.
    public AbilityParams chosenAbility;

    // Start is called before the first frame update
    void Start()
    {
        if (isItemPedestal)
        {
            foreach (ItemSOInst item in items)
            {
                switch (item.rarity)
                {
                    case 0:
                        qual1Items.Add(item);
                        break;
                    case 1:
                        qual2Items.Add(item);
                        break;
                    case 2:
                        qual3Items.Add(item);
                        break;
                }
            }


            if (!overrideRandomSelection) // If the pedestal is to select a random item, it does the following
            {
                // Finds the quality of item to spawn.
                weightsSum = 0;
                foreach (int weight in qualityWeights)
                {
                    weightsSum += weight;
                }

                int randomWacky = Random.Range(0, weightsSum);
                int currentWeightSum = 0;
                int chosenQuality = 0;
                for (int i = 0; i < qualityWeights.Length; i++)
                {
                    currentWeightSum += qualityWeights[i];
                    if (randomWacky <= currentWeightSum)
                    {
                        chosenQuality = i;
                        break;
                    }
                }

                List<ItemSOInst> itemListToUse = new List<ItemSOInst>();
                switch (chosenQuality)
                {
                    case 0:
                        itemListToUse = qual1Items;
                        break;
                    case 1:
                        itemListToUse = qual2Items;
                        break;
                    case 2:
                        itemListToUse = qual3Items;
                        break;
                }

                int randItemSel = Random.Range(0, itemListToUse.Count);
                chosenItem = itemListToUse[randItemSel];
            }

            if (chosenItem.itemMesh == null)
            {
                gameObject.GetComponent<MeshFilter>().mesh = EntityReferencerGuy.Instance.defaultWeaponMesh;
            }
            else
            {
                gameObject.GetComponent<MeshFilter>().mesh = chosenItem.itemMesh;
            }
        }
        else
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

                weightsSum = 0;
                foreach (AbilityParams ability in paramsToUse)
                {
                    weightsSum += ability.dropWeight;
                }

                int randomWacky = Random.Range(0, weightsSum);
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(45, 0, 35 * Mathf.Round(2 * Time.time));
    }

    public void StartPickup(GameObject pickerUpper)
    {
        if (isItemPedestal)
        {
            chosenItem.AddItemScript(pickerUpper);
            Destroy(gameObject);
        }
        else
        {
            pickerUpper.GetComponent<Attack>().abilityTypes[0] = chosenAbility;
            pickerUpper.GetComponent<Attack>().charges[0] = chosenAbility.maxCharges;
            pickerUpper.GetComponent<Attack>().coolDowns[0] = 0;
            Destroy(gameObject);
        }
    }
}
