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
    public int statusType;
    public float statusPosition;
    float statusTransConst = 0.7f;
    List<int> iconOrder = new List<int>();
    int index;

    void Start()
    {
        texty = Instantiate(statusText);
        texty.transform.SetParent(GameObject.Find("Canvas").transform);
        canvas = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().canvas;
    }

    public void SetPosition()
    {
        switch (statusType)
        {
            case 0:
                index = owner.GetComponent<Statuses>().iconOrder.IndexOf(0);
                owner.GetComponent<Statuses>().iconOrder.RemoveAt(index);
                break;
            case 1:
                index = owner.GetComponent<Statuses>().iconOrder.IndexOf(1);
                owner.GetComponent<Statuses>().iconOrder.RemoveAt(index);
                break;
        }
    }

    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
            Destroy(texty);
        }

        switch (statusType)
        {
            case 0:
                if (owner.GetComponent<Statuses>().bleedStacks == 0)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                    texty.transform.localScale = new Vector3(0, 0, 0);
                    SetPosition();
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    texty.transform.localScale = new Vector3(1, 1, 1);
                    texty.text = "x" + owner.GetComponent<Statuses>().bleedStacks;
                    iconOrder = owner.GetComponent<Statuses>().iconOrder;
                    index = iconOrder.IndexOf(0);
                    statusPosition = index;
                }
                break;
            case 1:
                if (owner.GetComponent<Statuses>().poisonStacks == 0)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                    texty.transform.localScale = new Vector3(0, 0, 0);
                    SetPosition();
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    texty.transform.localScale = new Vector3(1, 1, 1);
                    texty.text = "x" + owner.GetComponent<Statuses>().poisonStacks;
                    iconOrder = owner.GetComponent<Statuses>().iconOrder;
                    index = iconOrder.IndexOf(1);
                    statusPosition = index;
                }
                break;
        }

        statusPosition -= 0.5f * statusTransConst * owner.GetComponent<Statuses>().iconOrder.Count;

        transform.position = new Vector3(owner.transform.position.x + statusTransConst * statusPosition, owner.transform.position.y + 0.9f, -1);

        texty.transform.localPosition = 108 * (owner.transform.position + new Vector3(1.9f + statusTransConst * statusPosition, 0.5f,3));
    }
}
