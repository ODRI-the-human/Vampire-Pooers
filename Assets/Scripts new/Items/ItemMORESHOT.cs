using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORESHOT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Attack>().noExtraShots++;
        
        if (gameObject.tag == "Hostile" && gameObject.GetComponent<Attack>().noExtraShots == 1)
        {
            gameObject.GetComponent<Attack>().shotAngleCoeff += 1.3f;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().noExtraShots--;
        if (gameObject.tag == "Hostile" && gameObject.GetComponent<Attack>().noExtraShots == 0)
        {
            gameObject.GetComponent<Attack>().shotAngleCoeff -= 1.3f;
        }
    }
}
