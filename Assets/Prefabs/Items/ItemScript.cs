using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    public int instances = 0; // the number of instances of the item held.
    public GameObject[] objectsToUse;
    public AbilityParams[] abilitiesToUse;

    public abstract void AddInstance(); // applies any stat changes n shit that the item needs to do; for HP, fire rate ups, and such
    public abstract void RemoveInstance();
    public abstract void OnHit(GameObject victim, GameObject responsible);
    public abstract void OnKill();
    public abstract void OnHurt();
    public abstract void OnLevel();
    public abstract float DamageMult(); // Called for damage mult calculations.
}
