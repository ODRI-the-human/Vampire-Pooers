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
    GameObject camera;
    public int statusType;
    public float statusPosition;
    float statusTransConst = 0.7f;
    List<int> iconOrder = new List<int>();
    int index = -1;

    void Start()
    {
        texty = Instantiate(statusText);
        texty.transform.SetParent(GameObject.Find("Canvas").transform);
        canvas = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().canvas;
        camera = GameObject.Find("Main Camera");
    }

    public void SetPosition()
    {
        if (index != -1)
        {
            index = owner.GetComponent<Statuses>().iconOrder.IndexOf(statusType);
            owner.GetComponent<Statuses>().iconOrder.RemoveAt(index);
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
            case 2:
                if (owner.GetComponent<Statuses>().hasElectric == 0)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                    texty.transform.localScale = new Vector3(0, 0, 0);
                    SetPosition();
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    texty.transform.localScale = new Vector3(1, 1, 1);
                    texty.text = "";
                    iconOrder = owner.GetComponent<Statuses>().iconOrder;
                    index = iconOrder.IndexOf(2);
                    statusPosition = index;
                }
                break;
            case 3:
                if (owner.GetComponent<NewPlayerMovement>().isSlowed == 0)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                    texty.transform.localScale = new Vector3(0, 0, 0);
                    SetPosition();
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    texty.transform.localScale = new Vector3(1, 1, 1);
                    texty.text = "";
                    iconOrder = owner.GetComponent<Statuses>().iconOrder;
                    index = iconOrder.IndexOf(3);
                    statusPosition = index;
                }
                break;
        }

        statusPosition -= 0.5f * (owner.GetComponent<Statuses>().iconOrder.Count - 1);

        transform.position = new Vector3(owner.transform.position.x + statusTransConst * statusPosition, owner.transform.position.y + 0.9f, -0.5f);

        texty.transform.localPosition = 108 * (owner.transform.position - camera.transform.position + new Vector3(1.9f + statusTransConst * statusPosition, 0.5f,3));
        texty.transform.localPosition = new Vector3(texty.transform.localPosition.x, texty.transform.localPosition.y, 324); // done on a separate line, to keep it simple (stupid)
    }
}
