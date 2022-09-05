using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Icons : MonoBehaviour
{
    public GameObject owner;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI texty;
    public GameObject canvas;

    void Start()
    {
        statusText = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().StatusText;
        texty = Instantiate(statusText);
        texty.transform.SetParent(GameObject.Find("Canvas").transform);
        canvas = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().canvas;
    }

    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
            Destroy(texty);
        }

        if (owner.GetComponent<Bleed>().stacks == 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
            texty.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            texty.transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + 1.3f, 0);
        texty.text = "x" + owner.GetComponent<Bleed>().stacks;

        texty.transform.localPosition = 108 * (owner.transform.position + new Vector3(1.9f,0.9f,3));

        Debug.Log(canvas.transform.TransformPoint(owner.transform.position).ToString() + " / " + (owner.transform.position).ToString());
    }
}
