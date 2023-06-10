using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managePlayer : MonoBehaviour
{
    public int playerID = 1;

    public Material player2Mat;
    public Material player3Mat;
    public Material player4Mat;

    public void SetMaterial(int sauceID)
    {
        Material sus = null;

        switch (sauceID)
        {
            case 2:
                sus = player2Mat;
                break;
            case 3:
                sus = player3Mat;
                break;
            case 4:
                sus = player4Mat;
                break;
        }

        if (sauceID != 1)
        {
            GameObject model = GameObject.Find("cringe");
            MeshRenderer[] renderers = model.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rendy in renderers)
            {
                rendy.material = sus;
            }
        }
    }
}
