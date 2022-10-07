using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherStuff : MonoBehaviour
{
    public Component[] scripts;
    int numNormalScripts = 12;
    int poohead;

    void Update()
    {

    }

    public void Sprinkle(int which)
    {
        Invoke(nameof(DeleteItems), 0);
        poohead = which;
    }

    public void DeleteItems()
    {
        int counterGuy = 0;
        switch (poohead)
        {
            case 0: // removes all items.
                scripts = GetComponents(typeof(MonoBehaviour));
                foreach (Component thing in scripts)
                {
                    counterGuy++;
                    if (counterGuy > numNormalScripts && counterGuy != scripts.Length) // Only does the following for the 13th script onwards, and doesn't do it for the last script.
                    {
                        Destroy(thing);
                    }
                }
                break;
            case 1: // removes first item.
                break;
        }
    }
}
