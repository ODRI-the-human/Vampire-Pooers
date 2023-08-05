using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setAbilityCharges : MonoBehaviour
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
        int numCharges = 0;
        numCharges = owner.GetComponent<Attack>().charges[ability];

        if ((numCharges == 0) || (owner.GetComponent<Attack>().abilityTypes[ability].maxCharges == 1))
        {
            texta.text = "";
        }
        else
        {
            texta.text = numCharges.ToString();
        }
    }
}
