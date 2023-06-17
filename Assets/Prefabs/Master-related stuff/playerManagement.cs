using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerManagement : MonoBehaviour
{
    public GameObject[] players;
    public GameObject playerPrefab;
    public GameObject barry63;
    public int numPlayers = 0;
    public List<int> reviveQueue = new List<int>();

    public List<int> player1Items = new List<int>();
    public List<int> player2Items = new List<int>();
    public List<int> player3Items = new List<int>();
    public List<int> player4Items = new List<int>();
    
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    bool canStillSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        numPlayers = 0;
        OnPlayerJoined();
    }

    public void OnPlayerJoined()
    {
        GetPlayers();
        numPlayers = players.Length;

        //Debug.Log("num players: " + numPlayers.ToString());

        foreach (GameObject player in players)
        {
            if (!player.GetComponent<managePlayer>().IDSet) // if the ID has not yet been set:
            {
                //Debug.Log("new player ID: " + numPlayers.ToString());
                player.GetComponent<managePlayer>().playerID = numPlayers;
                player.GetComponent<managePlayer>().IDSet = true;
            }
        }

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
            Debug.Log("for iteration: " + i.ToString());

            switch (i)
            {
                case 0:
                    player = player1;
                    items = player1Items;
                    break;
                case 1:
                    player = player2;
                    items = player2Items;
                    break;
                case 2:
                    player = player3;
                    items = player3Items;
                    break;
                case 3:
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
                if (reviveQueue.Count != 0 && i == reviveQueue[0] - 1)
                {
                    player = Instantiate(playerPrefab);
                    player.GetComponent<ItemHolder>().itemsHeld = items;
                    player.GetComponent<managePlayer>().playerID = i + 1;
                }
            }

            switch (i)
            {
                case 0:
                    player1 = player;
                    player1Items = items;
                    break;
                case 1:
                    player2 = player;
                    player2Items = items;
                    break;
                case 2:
                    player3 = player;
                    player3Items = items;
                    break;
                case 3:
                    player4 = player;
                    player4Items = items;
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

            gameObject.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject.Find("Main Camera").GetComponent<cameraMovement>().players = players;
    }
}