using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSOs/ItemSO")]
public class ItemSOInst : ScriptableObject
{
    public int rarity = 0;
    public GameObject[] objectsUsedByItem; // item scripts reference this array to get which objects to use
    public AbilityParams[] abilitiesUsedByItem; // item scripts reference this array to get which abilities to use, for shit like daggerthrow
    public bool addToProjectiles = false; // Only make this true if the item actually changes how the projectile moves, like bouncing or homing.
    public MonoScript itemScript;
    public Mesh itemMesh;
    public string name = "Itemminahahahahhaha";
    public string description = "on hit you beomcing rreally cool!";
    public string flavourText = "yummy text";

    public void AddItemScript(GameObject itemOwner)
    {
        itemOwner.GetComponent<ItemHolder2>().ApplyItem(this);
    }
}
