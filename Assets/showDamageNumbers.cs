using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showDamageNumbers : MonoBehaviour
{
    public TextMeshProUGUI DamageNumber;
    public TextMeshProUGUI texty;
    public GameObject canvas;

    void Start()
    {
        canvas = gameObject.GetComponent<EntityReferencerGuy>().canvas;
    }

    public void showDamage(Vector3 pos, float damage, int damageType)
    {
        texty = Instantiate(DamageNumber);
        texty.transform.SetParent(GameObject.Find("Canvas").transform);
        texty.transform.localPosition = 108 * (pos + new Vector3(1.9f + Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0));
        texty.text = damage.ToString();

        switch (damageType)
        {
            case (int)DAMAGETYPES.NORMAL:
                break;
            case (int)DAMAGETYPES.BLEED:
                texty.GetComponent<TextMeshProUGUI>().color = Color.red;
                break;
            case (int)DAMAGETYPES.POISON:
                texty.GetComponent<TextMeshProUGUI>().color = Color.green;
                break;
            case (int)DAMAGETYPES.ELECTRIC:
                texty.GetComponent<TextMeshProUGUI>().color = Color.blue;
                break;
        }
    }
}
