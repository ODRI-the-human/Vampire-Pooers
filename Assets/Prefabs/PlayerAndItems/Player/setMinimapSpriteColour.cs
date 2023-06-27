using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMinimapSpriteColour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material mat = gameObject.GetComponentInParent<managePlayer>().chosenMaterial;
        Color colly = mat.color;
        gameObject.GetComponent<SpriteRenderer>().color = colly;
    }
}
