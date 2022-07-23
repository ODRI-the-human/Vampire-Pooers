using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public int itemChosen;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        itemChosen = (int)Random.Range(-0.5f, 21.5f);
        spriteRenderer.sprite = spriteArray[itemChosen];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("item");
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }
    }
}
