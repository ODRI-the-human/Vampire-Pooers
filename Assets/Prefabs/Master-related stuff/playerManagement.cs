using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    // Stores the instances of players' UI.
    public GameObject playerUI;
    public GameObject player1UI;
    public GameObject player2UI;
    public GameObject player3UI;
    public GameObject player4UI;

    // These are the materials the player is assigned. These are assigned in the players' scripts, BUT we use these for colouring the HP bars.
    public Material player2Mat;
    public Material player3Mat;
    public Material player4Mat;

    bool canStillSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        numPlayers = 0;
        //OnPlayerJoined();
    }

    public void OnPlayerJoined()
    {
        Debug.Log("player joinersd");
        GetPlayers();
        numPlayers = players.Length;
        GameObject newPlayer = null;

        //Debug.Log("num players: " + numPlayers.ToString());

        foreach (GameObject player in players)
        {
            if (!player.GetComponent<managePlayer>().IDSet) // if the ID has not yet been set:
            {
                //Debug.Log("new player ID: " + numPlayers.ToString());
                newPlayer = player;
                player.GetComponent<getItemDescription>().itemsExist = true;
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

        CreateNewPlayerUI(newPlayer);//, newPlayer.GetComponent<managePlayer>().playerID);

        if (numPlayers == 2) // For now I'll just make it so 2-player co-op is possible, just to make UI shit easier. In future I'll add 4 player co-op.
        {
            gameObject.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    void CreateNewPlayerUI(GameObject newPlayer)//, int playerID)
    {
        GameObject newPlayerUI = Instantiate(playerUI);
        newPlayerUI.transform.SetParent(gameObject.GetComponent<EntityReferencerGuy>().canvasInnerBound.transform);//SetParent(EntityReferencerGuy.Instance.canvas.transform);
        newPlayerUI.GetComponent<setUIOwner>().player = newPlayer;

        transform.Find("HPVisBar");

        switch (newPlayer.GetComponent<managePlayer>().playerID)
        {
            case 1:
                player1UI = newPlayerUI;
                newPlayerUI.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                newPlayerUI.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);

                player1UI.transform.Find("itemVisBox").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(380 * 2, 114);
                player1UI.GetComponent<setUIOwner>().itemSprites.SendMessage("UpdateVisual");
                break;
            case 2:
                player2UI = newPlayerUI;
                player2UI.GetComponent<setUIOwner>().playerMat = player2Mat;
                newPlayerUI.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
                newPlayerUI.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);

                player1UI.transform.Find("itemVisBox").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(380, 114);
                player2UI.transform.Find("itemVisBox").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(380, 114);
                player1UI.GetComponent<setUIOwner>().itemSprites.SendMessage("UpdateVisual");
                player2UI.GetComponent<setUIOwner>().itemSprites.SendMessage("UpdateVisual");
                break;
            case 3:
                player3UI = newPlayerUI;
                player3UI.GetComponent<setUIOwner>().playerMat = player3Mat;

                player2UI.GetComponent<RectTransform>().anchorMin = new Vector2(0.333f, 1);
                player2UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.333f, 1);
                player3UI.GetComponent<RectTransform>().anchorMin = new Vector2(0.666f, 1);
                player3UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.666f, 1);

                player1UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                player2UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                player3UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                break;
            case 4:
                player4UI = newPlayerUI;
                player4UI.GetComponent<setUIOwner>().playerMat = player4Mat;

                player2UI.GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 1);
                player2UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.25f, 1);
                player3UI.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
                player3UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
                player4UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 1);
                player4UI.GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 1);

                player1UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                player2UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                player3UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                player4UI.transform.Find("itemVisBox").gameObject.SetActive(false);
                break;
        }

        newPlayerUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void SetPlayerStates()
    {
        //Debug.Log("shoulda done marty");
        //if (!player1Alive)
        //{
        //    player1 = Instantiate(playerPrefab);
        //    player1.GetComponent<ItemHolder>().itemsHeld = player1Items;
        //    player1Alive = true;
        //    GetPlayers();
        //}

        //GameObject player = null;
        //List<int> items = new List<int>();
        Debug.Log("new Round Andy");

        if (reviveQueue.Count != 0)
        {
            List<int> items = new List<int>();

            switch (reviveQueue[0])
            {
                case 1:
                    items = player1Items;
                    break;
                case 2:
                    items = player2Items;
                    break;
                case 3:
                    items = player3Items;
                    break;
                case 4:
                    items = player4Items;
                    break;
            }

            GameObject player = Instantiate(playerPrefab);
            player.GetComponent<ItemHolder>().itemsHeld = items;
            player.GetComponent<managePlayer>().playerID = reviveQueue[0];
            player.GetComponent<managePlayer>().IDSet = true;

            //switch (reviveQueue[0])
            //{
            //    case 1:
            //        player1 = player;
            //        break;
            //    case 2:
            //        player2 = player;
            //        break;
            //    case 3:
            //        player3 = player;
            //        break;
            //    case 4:
            //        player4 = player;
            //        break;
            //}

            reviveQueue.RemoveAt(0);
            GetPlayers();
        }

        if (player1 != null)
        {
            player1Items = player1.GetComponent<ItemHolder>().itemsHeld;
        }
        if (player2 != null)
        {
            player2Items = player2.GetComponent<ItemHolder>().itemsHeld;
        }
        if (player3 != null)
        {
            player3Items = player3.GetComponent<ItemHolder>().itemsHeld;
        }
        if (player4 != null)
        {
            player4Items = player4.GetComponent<ItemHolder>().itemsHeld;
        }
        //for (int i = 0; i < numPlayers; i++)
        //{
        //    Debug.Log("for iteration: " + i.ToString());

        //    switch (i)
        //    {
        //        case 0:
        //            player = player1;
        //            items = player1Items;
        //            break;
        //        case 1:
        //            player = player2;
        //            items = player2Items;
        //            break;
        //        case 2:
        //            player = player3;
        //            items = player3Items;
        //            break;
        //        case 3:
        //            player = player4;
        //            items = player4Items;
        //            break;
        //    }

        //    if (player == null)
        //    {
        //        if (reviveQueue.Count != 0 && i == reviveQueue[0] - 1)
        //        {
        //            player = Instantiate(playerPrefab);
        //            player.GetComponent<ItemHolder>().itemsHeld = items;
        //            player.GetComponent<managePlayer>().playerID = i + 1;
        //            reviveQueue.RemoveAt(0);
        //        }
        //    }

        //Debug.Log("Player: " + player.ToString());

        //items = player.GetComponent<ItemHolder>().itemsHeld;

        ////Debug.Log("Player: " + (i + 1).ToString() + " / Items: " + items.Count.ToString());

        //switch (i)
        //{
        //    case 0:
        //        player1 = player;
        //        player1Items = items;
        //        break;
        //    case 1:
        //        //player2 = player;
        //        player2Items = items;
        //        break;
        //    case 2:
        //        //player3 = player;
        //        player3Items = items;
        //        break;
        //    case 3:
        //        //player4 = player;
        //        player4Items = items;
        //        break;
        //}
        //}
        //GetPlayers();
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
                Invoke(nameof(GetPlayers), 0.1f);
            }

            gameObject.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    void GetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject.Find("Main Camera").GetComponent<cameraMovement>().players = players;

        GameObject currentPlayer = null;
        int ID = 0;

        for (int i = 0; i < players.Length; i++)
        {
            currentPlayer = players[i];
            ID = currentPlayer.GetComponent<managePlayer>().playerID;
            switch (ID)
            {
                case 1:
                    player1 = currentPlayer;
                    break;
                case 2:
                    player2 = currentPlayer;
                    break;
                case 3:
                    player3 = currentPlayer;
                    break;
                case 4:
                    player4 = currentPlayer;
                    break;
            }
        }
    }
}
