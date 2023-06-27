using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setFogTexture : MonoBehaviour
{
    public Texture2D fogTex;
    int timer = 0;

    int radius = 50;
    public GameObject[] players;
    public GameObject master;
    
    // Start is called before the first frame update
    void Start()
    {
        fogTex = new Texture2D(300, 300);
        master = EntityReferencerGuy.Instance.master;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer % 50 == 0)
        {
            players = master.GetComponent<playerManagement>().players;

            float rSquared = radius * radius;

            foreach (GameObject player in players)
            {
                Debug.Log("didTheTexAndy");

                int x = Random.Range(0, 300);//5 * Mathf.RoundToInt(player.transform.position.x);
                int y = Random.Range(0, 300);//5 * Mathf.RoundToInt(player.transform.position.y);

                for (int u = x - radius; u < x + radius + 1; u++)
                    for (int v = y - radius; v < y + radius + 1; v++)
                        if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                            fogTex.SetPixel(u, v, new Color(1, 0, 0, 1));
            }
        }

        timer++;
    }

    //public static Texture2D DrawCircle(this Texture2D tex, int x, int y, int radius = 3)
    //{
    //    float rSquared = radius * radius;

    //    for (int u = x - radius; u < x + radius + 1; u++)
    //        for (int v = y - radius; v < y + radius + 1; v++)
    //            if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
    //                tex.SetPixel(u, v, new Color(1, 1, 1, 1));

    //    return tex;
    //}
}
