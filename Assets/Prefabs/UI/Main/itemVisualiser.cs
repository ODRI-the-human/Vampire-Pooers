using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemVisualiser : MonoBehaviour
{
    public TextMeshProUGUI texta;
    public bool isMaster;
    public int[] itemStacks = new int[0];
    public List<int> itemPresOrder = new List<int>();
    public List<GameObject> itemNumbers = new List<GameObject>();
    public GameObject itemNumberPrefab;
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        if (isMaster)
        {
            owner = EntityReferencerGuy.Instance.master;
            owner.GetComponent<EntityReferencerGuy>().itemsHeldVisualiser = gameObject;
        }
        else
        {
            owner = gameObject.GetComponentInParent<setUIOwner>().player;
            owner.GetComponent<managePlayer>().itemsHeldVisualiser = gameObject;
        }
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (GameObject go in itemNumbers)
        {
            Destroy(go);
        }

        itemNumbers.Clear();

        if (!isMaster)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x + 1, 150);
        }
        itemStacks = new int[60];
        string stringToPass = "";
        itemPresOrder.Clear();

        foreach (int item in owner.GetComponent<ItemHolder>().itemsHeld)
        {
            EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = item;
            EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
            if (itemStacks[item] == 0)
            {
                stringToPass += "<sprite=" + item.ToString() + ">";
                itemPresOrder.Add(item);
            }
            itemStacks[item]++;
        }

        int numStacks = 0;
        int level = 0; //Needed for placing item numbers at the proper y coordinate. In future this could probably be better done, but this should work for now.
        GameObject itemNo = null;
        int i = 0;

        int maxNumOnLevel = Mathf.RoundToInt(transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x) / 38;
        int maxLevel = Mathf.FloorToInt(itemPresOrder.Count / maxNumOnLevel);

        foreach (int item in itemPresOrder)
        {
            if (itemStacks[item] > 1)
            {
                itemNo = Instantiate(itemNumberPrefab);
                itemNo.GetComponent<TextMeshProUGUI>().text = "x" + itemStacks[item].ToString();
                itemNo.transform.SetParent(gameObject.transform.parent.transform);
                itemNo.transform.localScale = new Vector3(1, 1, 1);

                level = Mathf.FloorToInt(i / maxNumOnLevel);
                Debug.Log("Level: " + level.ToString());

                if (isMaster)
                {
                    itemNo.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
                    itemNo.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
                    itemNo.transform.localPosition = new Vector2(38f * i - (38f * maxNumOnLevel * level) - 38f * 2f, 38f * (maxLevel + 1 - level));
                    itemNumbers.Add(itemNo);
                }
                else
                {
                    itemNo.transform.localPosition = new Vector2(38f * i - (38f * maxNumOnLevel * level), -38f * level);
                    itemNumbers.Add(itemNo);
                }
            }

            i++;
        }

        texta.text = stringToPass;
    }

        //Debug.Log("ok");
        //foreach (GameObject go in itemSprites)
        //{
        //    Destroy(go);
        //}

        //itemSprites.Clear();

        //GameObject thisSprite = null;
        //int i = 0;
        //foreach (int item in itemsHeld)
        //{
        //    EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = item;
        //    EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
        //    if (EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().quality != (int)ITEMTIERS.WEAPON)
        //    {
        //        thisSprite = Instantiate(itemSprite);
        //        thisSprite.GetComponent<SpriteRenderer>().sprite = EntityReferencerGuy.Instance.itemSprites[item];
        //        thisSprite.transform.SetParent(gameObject.transform);
        //        thisSprite.transform.localPosition = new Vector2(i * 5, 0);
        //        itemSprites.Add(thisSprite);
        //        i++;
        //    }
        //}
}
