using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showDamageNumbers : MonoBehaviour
{
    public TextMeshProUGUI DamageNumber;
    public TextMeshProUGUI texty;
    public GameObject canvas;
    public Rigidbody2D rb;
    GameObject camera;

    void Start()
    {
        canvas = gameObject.GetComponent<EntityReferencerGuy>().canvas;
        camera = GameObject.Find("Main Camera");
    }

    public void showDamage(Vector3 pos, float damage, int damageType)
    {
        texty = Instantiate(DamageNumber);
        texty.transform.SetParent(GameObject.Find("Canvas").transform);
        texty.transform.localPosition = 108 * (pos - camera.transform.position + new Vector3(1.9f, 0, 0));
        texty.transform.localPosition = new Vector3(texty.transform.localPosition.x, texty.transform.localPosition.y, 324); // done on a separate line, to keep it simple (stupid)
        texty.text = (Mathf.RoundToInt(damage)).ToString();

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
