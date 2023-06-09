using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose5ItemTemp : MonoBehaviour
{
    public int numItemsToLose = 5;
    public List<int> roundsLeft = new List<int>(); // Just so the curse can stack, with the timers not having to expire at the same time.

    // Start is called before the first frame update
    void Start()
    {
        roundsLeft.Add(2);
    }

    void Update()
    {
        if (roundsLeft[0] <= 0) // Only removes the first one, which is fine since if they should end at the same time the second will just be removed one frame later.
        {
            roundsLeft.RemoveAt(0);
            numItemsToLose -= 5;
        }

        if (roundsLeft.Count == 0)
        {
            Destroy(this);
        }
    }

    public void OnHurtEffects()
    {
        gameObject.SendMessage("Undo");
        for (int i = 0; i < numItemsToLose; i++)
        {
            if (gameObject.GetComponent<ItemHolder>().itemsHeld.Count > 0)
            {
                int itemIndex = Random.Range(0, gameObject.GetComponent<ItemHolder>().itemsHeld.Count);
                gameObject.GetComponent<ItemHolder>().itemsHeld.RemoveAt(itemIndex);
            }
        }

        Invoke(nameof(ApplyItems), 0.001f);
    }

    void ApplyItems()
    {
        gameObject.GetComponent<ItemHolder>().ApplyAll();
        Destroy(this);
    }

    public void newWaveEffects()
    {
        for (int i = 0; i < roundsLeft.Count; i++)
        {
            roundsLeft[i]--;
        }
    }
}
