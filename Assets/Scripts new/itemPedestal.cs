using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public int itemChosen;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    int minRange = 0;
    int maxRange = 24;
    GameObject[] gos;

    // Start is called before the first frame update
    void Start()
    {
        itemChosen = Random.Range(minRange, maxRange);
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
