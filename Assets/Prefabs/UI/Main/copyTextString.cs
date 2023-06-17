using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class copyTextString : MonoBehaviour
{
    public TextMeshProUGUI ownText;
    public GameObject textToCopy;

    // Update is called once per frame
    void Update()
    {
        ownText.text = textToCopy.GetComponent<TextMeshProUGUI>().text;
    }
}
