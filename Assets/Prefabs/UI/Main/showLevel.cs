using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showLevel : MonoBehaviour
{
    public GameObject owner;
    public TextMeshProUGUI texta;

    // Update is called once per frame
    void Update()
    {
        texta.text = "LVL " + owner.GetComponent<LevelUp>().level.ToString();
    }
}