using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showLevel : MonoBehaviour
{
    public GameObject owner;
    public TextMeshProUGUI texta;

    void Start()
    {
        owner = transform.parent.gameObject.GetComponent<setUIOwner>().player;
    }

    void Update()
    {
        texta.text = "LVL " + owner.GetComponent<LevelUp>().level.ToString();
    }
}