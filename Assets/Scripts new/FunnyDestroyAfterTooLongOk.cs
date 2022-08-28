using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is just to kill any instances of SFX, might help keep CPU load down in the case that this game is developed enough to require any CPU at all

public class SFXkiller : MonoBehaviour
{

    private int Timer = 0;

    // Update is called once per frame
    void Update()
    {
        Timer += 1;
        if (Timer > 10)
        {
            Destroy(gameObject);
        }
    }
}
