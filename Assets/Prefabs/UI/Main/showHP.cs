using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showHP : MonoBehaviour
{
    public GameObject owner;
    public TextMeshProUGUI texta;
    public int maxOrCurrent;

    // Update is called once per frame
    void Update()
    {
        switch (maxOrCurrent)
        {
            case 0:
                texta.text = "/" + Mathf.Ceil(owner.GetComponent<HPDamageDie>().MaxHP).ToString();
                break;
            case 1:
                texta.text = Mathf.Ceil(owner.GetComponent<HPDamageDie>().HP).ToString();
                break;
        }
        //texta.text = Mathf.Round(owner.GetComponent<HPDamageDie>().HP).ToString() + " / " + Mathf.Round(owner.GetComponent<HPDamageDie>().MaxHP).ToString();
    }
}
