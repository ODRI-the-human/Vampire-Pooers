using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setCurseText : MonoBehaviour
{
    public TextMeshProUGUI texta;
    public GameObject owner;

    void Start()
    {
        owner = gameObject.GetComponentInParent<setUIOwner>().player;
    }

    void Update()
    {
        texta.text = owner.GetComponent<getItemDescription>().curseDescription;
    }
}
