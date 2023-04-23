using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPOISONSPLOSM : MonoBehaviour
{
    public int instances = 1;
    public GameObject poisonSplosm;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            EventManager.DeathEffects += SpawnPoison;
            
            Debug.Log("Added poison wahoo");
        }
        poisonSplosm = EntityReferencerGuy.Instance.poisonSplosm;
    }

    public void SpawnPoison(Vector3 pos)
    {
        GameObject Marty = Instantiate(poisonSplosm, pos, transform.rotation);
        Marty.transform.localScale *= (0.5f + instances * 0.5f);
        Marty.GetComponent<TriggerPoison>().owner = gameObject;
    }

    public void Undo()
    {
        EventManager.DeathEffects -= SpawnPoison;
        Destroy(this);
    }
}
