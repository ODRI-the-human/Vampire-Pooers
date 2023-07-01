using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBackgroundColor : MonoBehaviour
{
    public int qualityChosen;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke(nameof(LateStart), 0.02f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    public void SetColour()
    {
        Debug.Log("color moment");
        //GameObject parent = transform.parent.gameObject;
        //qualityChosen = parent.GetComponent<itemPedestal>().chosenQuality;

        switch (qualityChosen)
        {
            case (int)ITEMTIERS.COMMON:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case (int)ITEMTIERS.UNCOMMON:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case (int)ITEMTIERS.LEGENDARY:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case (int)ITEMTIERS.NULL:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 1, 1);
                break;
            case (int)ITEMTIERS.WEAPON:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
    }
}
