using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showAbilityTimer : MonoBehaviour
{
    public int ability;
    public GameObject owner;
    public TextMeshProUGUI texta;

    void Start()
    {
        owner = gameObject.GetComponentInParent<setUIOwner>().player;
    }

    void Update()
    {
        float remainingTime = 0;
        remainingTime = Mathf.Ceil(owner.GetComponent<Attack>().coolDowns[ability] / 50f);

        if (remainingTime <= 0)
        {
            texta.text = "";
        }
        else
        {
            texta.text = remainingTime.ToString();
        }
    }
}
