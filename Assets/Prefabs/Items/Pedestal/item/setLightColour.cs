using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setLightColour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LateStart), 0.02f);
    }

    void LateStart()
    {
        GameObject parent = transform.parent.gameObject;
        GameObject parentParent = parent.transform.parent.gameObject;
        int curseType = parentParent.GetComponent<itemPedestal>().curseType;

        switch (curseType)
        {
            case 0: // Gives player three of the item, gives enemies one.
                gameObject.GetComponent<Light>().color = Color.red;
                break;
            case 1: // Get 1 of this item every time you pick up an item, lose 2 items on hit (perm)
                gameObject.GetComponent<Light>().color = Color.blue;
                break;
            case 2: // Get 3 of the item, lose 5 random items (ONCE) if you get hit in the next 2 rounds.
                gameObject.GetComponent<Light>().color = Color.cyan;
                break;
            case 3: // Give enemies one of the item, if an enemy dies in the next 2 rounds they can drop an item they hold.
                gameObject.GetComponent<Light>().color = Color.green;
                break;
            case 4: // Get five of the item, but can't heal ever again.
                gameObject.GetComponent<Light>().color = Color.magenta;
                break;
            case 5: // Get five of the item, but die instantly if hit in the next 2 rounds.
                gameObject.GetComponent<Light>().color = Color.yellow;
                break;
            case 6: // gives 3 of the item.
                gameObject.GetComponent<Light>().color = new Color(1, 0.647f, 0, 1);
                break;
            case 7: // gives 10 of the item.
                gameObject.GetComponent<Light>().color = new Color(1, 0.843f, 0, 1);
                break;
        }

        if (curseType != -2)
        {
            gameObject.GetComponent<Light>().intensity = 40;
        }
    }
}
