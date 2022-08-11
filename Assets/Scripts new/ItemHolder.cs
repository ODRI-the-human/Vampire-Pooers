using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    int timerBullshit;

    // Update is called once per frame
    void Update()
    {
        timerBullshit++;
        if (timerBullshit == 200)
        {
            gameObject.AddComponent<ItemHP25>();
        }
    }
}
