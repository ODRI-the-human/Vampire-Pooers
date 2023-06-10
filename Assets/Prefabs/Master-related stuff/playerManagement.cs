using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManagement : MonoBehaviour
{
    public GameObject[] players;
    public GameObject playerPrefab;
    public GameObject barry63;
    public int numPlayers = 2;
    public List<int> reviveQueue = new List<int>();

    public List<int> player1Items = new List<int>();
    public List<int> player2Items = new List<int>();
    public List<int> player3Items = new List<int>();
    public List<int> player4Items = new List<int>();
    
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    // Start is called before the first frame update
    void Start()
    {
        GetPlayers();
        numPlayers = players.Length;

        int ID = 1;

        foreach (GameObject player in players)
        {
            player.GetComponent<managePlayer>().playerID = ID;
            player.GetComponent<managePlayer>().SetMaterial(ID);
            ID++;
        }
    }

    public void NewRoundStarted()
    {
        //Debug.Log("shoulda done marty");
        //if (!player1Alive)
        //{
        //    player1 = Instantiate(playerPrefab);
        //    player1.GetComponent<ItemHolder>().itemsHeld = player1Items;
        //    player1Alive = true;
        //    GetPlayers();
        //}

        GameObject player = null;
        List<int> items = new List<int>();

        for (int i = 0; i < numPlayers; i++)
        {
            switch (i)
            {
                case 1:
                    player = player1;
                    items = player1Items;
                    break;
                case 2:
                    player = player2;
                    items = player2Items;
                    break;
                case 3:
                    player = player3;
                    items = player3Items;
                    break;
                case 4:
                    player = player4;
                    items = player4Items;
                    break;
            }

            if (player != null)
            {
                items = player.GetComponent<ItemHolder>().itemsHeld;
            }
            else
            {
                if (reviveQueue.Count != 0 && i == reviveQueue[0])
                {
                    player = Instantiate(playerPrefab);
                    player.GetComponent<ItemHolder>().itemsHeld = items;
                    player.GetComponent<managePlayer>().playerID = i;
                }
            }

            switch (i)
            {
                case 1:
                    player1 = player;
                    player1Items = items;
                    break;
                case 2:
                    player2 = player;
                    player2Items = items;
                    break;
                case 3:
                    player2 = player;
                    player2Items = items;
                    break;
                case 4:
                    player3 = player;
                    player3Items = items;
                    break;
            }
        }

        if (reviveQueue.Count != 0)
        {
            reviveQueue.RemoveAt(0);
        }

        GetPlayers();
    }

    void ApplyItemOnDeaths(GameObject victim)
    {
        if (victim.tag == "Player") // When a player dies!
        {
            reviveQueue.Add(victim.GetComponent<managePlayer>().playerID);


            if (players.Length == 1)
            {
                Instantiate(barry63, EntityReferencerGuy.Instance.camera.transform.position, Quaternion.Euler(0, 0, 0));
                EntityReferencerGuy.Instance.camera.GetComponent<cameraMovement>().moveCamera = false;
                EntityReferencerGuy.Instance.keepBestStatObj.GetComponent<keepBestScore>().ShowStats();
            }
            else
            {
                Invoke(nameof(GetPlayers), 0.2f);
            }
        }
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        EntityReferencerGuy.Instance.camera.GetComponent<cameraMovement>().players = players;

        foreach (GameObject player in players)
        {
            switch (player.GetComponent<managePlayer>().playerID)
            {
                case 1:
                    player1 = player;
                    break;
                case 2:
                    player2 = player;
                    break;
                case 3:
                    player3 = player;
                    break;
                case 4:
                    player4 = player;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
