using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFOURDIRMARTY : MonoBehaviour
{

    public int instances = 1;
    int numTimesFired = 0;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void OnShootEffects()
    {
        //Debug.Log("times fired: " + gameObject.GetComponent<Attack>().timesFired.ToString());
        //numTimesFired++;
        //if (numTimesFired % 4 == 0)
        //{
        //    for (int i = 1; i < instances * 4; i++)
        //    {
        //        gameObject.GetComponent<Attack>().currentAngle = i * (Mathf.PI) / (2 * instances);
        //        //Debug.Log("Family Guy: " + gameObject.GetComponent<Attack>().currentAngle.ToString());
        //        gameObject.GetComponent<Attack>().UseWeapon(true);
        //    }
        //}
    }

    public void Undo()
    {
        Destroy(this);
    }
}
