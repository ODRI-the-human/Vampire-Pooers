using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Icons : MonoBehaviour
{
    public int statusType; // Stores the type of status (i.e. bleed/poison/whatever) that this icon is representing
    public Sprite[] statusSprites;

    public int index;
    public int numStatusesApplied;
    GameObject parent;

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI texty;

    void Start()
    {
        texty = Instantiate(statusText);
        texty.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
        texty.transform.localScale = new Vector3(1, 1, 1);

        parent = transform.parent.gameObject;
        gameObject.GetComponent<SpriteRenderer>().sprite = statusSprites[statusType];

        GetNewPos();
    }

    void Update()
    {
        transform.position = parent.transform.position + new Vector3(((1 - numStatusesApplied) * 0.5f + index) * 0.5f, 1, 0);
        texty.transform.position = transform.position + new Vector3(0.25f, -0.3f, 0);
        if (parent.GetComponent<Statuses>().statusStacks[statusType] != 0)
        {
            texty.text = "x" + (parent.GetComponent<Statuses>().statusStacks[statusType]).ToString();
        }
        else
        {
            texty.text = "";
        }
    }

    public void GetNewPos()
    {
        numStatusesApplied = parent.GetComponent<Statuses>().statusOrders.Count;

        for (int i = 0; i < numStatusesApplied; i++)
        {
            if (parent.GetComponent<Statuses>().statusOrders[i] == statusType)
            {
                index = i;
                break;
            }
        }
    }
}

//public GameObject owner;
//public TextMeshProUGUI statusText;
//public TextMeshProUGUI texty;
//GameObject camera;
//public int statusType;
//public float statusPosition;
//float statusTransConst = 0.7f;
//List<int> iconOrder = new List<int>();
//int index = -1;

//void Start()
//{
//    texty = Instantiate(statusText);
//    texty.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
//    camera = GameObject.Find("Main Camera");
//}

//public void SetPosition()
//{
//    if (index != -1)
//    {
//        index = owner.GetComponent<Statuses>().iconOrder.IndexOf(statusType);
//        owner.GetComponent<Statuses>().iconOrder.RemoveAt(index);
//    }
//}

//void Update()
//{
//    if (owner == null)
//    {
//        Destroy(gameObject);
//        Destroy(texty);
//    }

//    switch (statusType)
//    {
//        case 0:
//            if (owner.GetComponent<Statuses>().bleedStacks == 0)
//            {
//                transform.localScale = new Vector3(0, 0, 0);
//                texty.transform.localScale = new Vector3(0, 0, 0);
//                SetPosition();
//            }
//            else
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                texty.transform.localScale = new Vector3(1, 1, 1);
//                texty.text = "x" + owner.GetComponent<Statuses>().bleedStacks;
//                iconOrder = owner.GetComponent<Statuses>().iconOrder;
//                index = iconOrder.IndexOf(0);
//                statusPosition = index;
//            }
//            break;
//        case 1:
//            if (owner.GetComponent<Statuses>().poisonStacks == 0)
//            {
//                transform.localScale = new Vector3(0, 0, 0);
//                texty.transform.localScale = new Vector3(0, 0, 0);
//                SetPosition();
//            }
//            else
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                texty.transform.localScale = new Vector3(1, 1, 1);
//                texty.text = "x" + owner.GetComponent<Statuses>().poisonStacks;
//                iconOrder = owner.GetComponent<Statuses>().iconOrder;
//                index = iconOrder.IndexOf(1);
//                statusPosition = index;
//            }
//            break;
//        case 2:
//            if (!owner.GetComponent<Statuses>().hasElectric)
//            {
//                transform.localScale = new Vector3(0, 0, 0);
//                texty.transform.localScale = new Vector3(0, 0, 0);
//                SetPosition();
//            }
//            else
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                texty.transform.localScale = new Vector3(1, 1, 1);
//                texty.text = "";
//                iconOrder = owner.GetComponent<Statuses>().iconOrder;
//                index = iconOrder.IndexOf(2);
//                statusPosition = index;
//            }
//            break;
//        case 3:
//            if (owner.GetComponent<NewPlayerMovement>().isSlowed == 0)
//            {
//                transform.localScale = new Vector3(0, 0, 0);
//                texty.transform.localScale = new Vector3(0, 0, 0);
//                SetPosition();
//            }
//            else
//            {
//                transform.localScale = new Vector3(1, 1, 1);
//                texty.transform.localScale = new Vector3(1, 1, 1);
//                texty.text = "";
//                iconOrder = owner.GetComponent<Statuses>().iconOrder;
//                index = iconOrder.IndexOf(3);
//                statusPosition = index;
//            }
//            break;
//    }

//    statusPosition -= 0.5f * (owner.GetComponent<Statuses>().iconOrder.Count - 1);

//    transform.position = new Vector3(owner.transform.position.x + statusTransConst * statusPosition, owner.transform.position.y + 0.9f, -0.5f);

//    texty.transform.position = transform.position + new Vector3(0.25f, -0.4f, 0);

//texty.transform.localPosition = (owner.transform.position - camera.transform.position + new Vector3(statusTransConst * statusPosition, 0.4f, 3)) * (Screen.currentResolution.height / (2 * camera.GetComponent<Camera>().orthographicSize));// + 0.02f, 0.04f, 3);
//texty.transform.localPosition = new Vector3(texty.transform.localPosition.x, texty.transform.localPosition.y, 324); // done on a separate line, to keep it simple (stupid)
