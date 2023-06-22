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

        switch(ability)
        {
            case 1:
                remainingTime = Mathf.Ceil(owner.GetComponent<secondaryAbility>().abilityOneCooldown / 50);
                break;
            case 2:
                remainingTime = Mathf.Ceil(owner.GetComponent<secondaryAbility>().abilityTwoCooldown / 50);
                break;
        }

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
