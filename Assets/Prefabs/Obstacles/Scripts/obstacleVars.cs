using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleVars : MonoBehaviour
{
    void OnBecameVisible()
    {
        enabled = true;
        EntityReferencerGuy.Instance.numVisibleWalls++;
    }

    void OnBecameInvisible()
    {
        enabled = false;
        EntityReferencerGuy.Instance.numVisibleWalls--;
    }
}
