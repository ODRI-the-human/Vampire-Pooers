using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setItemText : MonoBehaviour
{
    public TextMeshProUGUI texta;
    public GameObject owner;

    void Start()
    {
        owner = gameObject.GetComponentInParent<setUIOwner>().player;
    }

    void Update()
    {
        texta.text = owner.GetComponent<getItemDescription>().itemDescription;
    }
}
