using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setUIOwner : MonoBehaviour
{
    public GameObject player;
    public GameObject HPVisBar;
    public GameObject itemSprites;
    public Material playerMat; 

    void Start()
    {
        if (playerMat != null)
        {
            HPVisBar.GetComponent<Image>().color = playerMat.color;
        }
    }
}
