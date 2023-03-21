using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showDamageNumbers : MonoBehaviour
{
    public TextMeshProUGUI DamageNumber;
    public TextMeshProUGUI texty;
    public Rigidbody2D rb;
    string plusOrMinus;
    GameObject camera;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    public void showDamage(Vector3 pos, float damage, int damageType, bool isCrit)
    {
        if (damageType == (int)DAMAGETYPES.HEAL)
        {
            plusOrMinus = "+";
        }
        else
        {
            plusOrMinus = "-";
        }

        texty = Instantiate(DamageNumber);
        texty.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
        texty.transform.position = pos;
        texty.transform.localScale = new Vector3(1, 1, 1);
        texty.text = (plusOrMinus + Mathf.RoundToInt(damage)).ToString();

        switch (damageType)
        {
            case (int)DAMAGETYPES.NORMAL:
                break;
            case (int)DAMAGETYPES.BLEED:
                texty.GetComponent<TextMeshProUGUI>().color = Color.red;
                break;
            case (int)DAMAGETYPES.POISON:
                texty.GetComponent<TextMeshProUGUI>().color = new Color(0.1f, 0.8f, 0.1f, 1);
                break;
            case (int)DAMAGETYPES.ELECTRIC:
                texty.GetComponent<TextMeshProUGUI>().color = Color.blue;
                break;
            case (int)DAMAGETYPES.HEAL:
                texty.GetComponent<TextMeshProUGUI>().color = Color.green;
                break;
        }

        if (isCrit)
        {
            texty.GetComponent<TextMeshProUGUI>().color = new Color(1, 0.647f, 0, 1);
        }
    }
}
