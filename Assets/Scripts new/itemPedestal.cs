using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public int itemChosen;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    bool isFine = false;
    float minRange = -0.5f;
    float maxRange = 19.5f; // should be 22.5f for all current items okie
    GameObject[] gos;

    // Start is called before the first frame update
    void Start()
    {
        itemChosen = Mathf.RoundToInt(Random.Range(minRange, maxRange));
        spriteRenderer.sprite = spriteArray[itemChosen];
        gos = GameObject.FindGameObjectsWithTag("item");
    }

    void Update()
    {
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<itemPedestal>().itemChosen == itemChosen && go != gameObject)
            {
                Debug.Log("WOw, something was fucked up!!!!!!!!!!!!");
                itemChosen = Mathf.RoundToInt(Random.Range(minRange, maxRange));
                spriteRenderer.sprite = spriteArray[itemChosen];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }
    }
}
