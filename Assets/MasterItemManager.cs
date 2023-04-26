using UnityEngine;
using System.Collections;

public class MasterItemManager : MonoBehaviour
{
    public float stopWatchDebuffAmt = 1;
    public float stopWatchInstances = 0;

    void Start()
    {
        stopWatchInstances = 1;
    }
    
    void Update()
    {
        stopWatchDebuffAmt = 1 / stopWatchInstances;
    }
}