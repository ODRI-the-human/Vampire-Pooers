using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour // This is adapted from Six Dot's video on the topic, so big thanks to them.
{
    public GameObject floorObj;
    public GameObject outerWallObj;
    public GameObject FOWfloorObj;
    public Vector3 playerSpawnPos;

    public GameObject mapCam;
    public GameObject FOWCam1;
    public GameObject FOWCam2;

    // For spawning rooms, numroomsmean is the typical number of rooms, range is the deviation that can occur.
    public int numRoomsMean;
    public int numRoomsRange;
    public int roomHeightMin;
    public int roomHeightMax;
    public int roomWidthMin;
    public int roomWidthMax;
    public List<Vector2> roomPositions = new List<Vector2>();
    public List<Vector2> roomSizes = new List<Vector2>();
    public List<bool> roomsJoined = new List<bool>();
    public List<int> roomJoinResponsibles = new List<int>(); // Stores the origin of the walker that joined to each room (index = room origin)

    enum gridSpace { empty, floor, wall, room };
    public int[,] gridPosOwners;
    gridSpace[,] grid;
    public int mapHeight; // This will be equal to the width.
    struct walker
    {
        public Vector2 dir;
        public Vector2 pos;
        public int origin;
        public int size; // between 1 (for 1 block wide) up to 3.
        public bool hasJoined;
    }
    List<walker> walkers;
    float chanceWalkerChangeDir = 0.5f;
    float chanceWalkerSpawn = 0.005f;
    float chanceWalkerDestroy = 0.005f;
    float chanceWalkerChangeSize = 0.01f;
    float chanceSpawnFiller = 0.35f;
    int maxWalkers = 10;
    public float percentToFill = 0.3f;
    public GameObject wallObj;
    public GameObject pathObj;
    public GameObject roomVisObj;

    GameObject spawnedObject; // Needed for 'storing' the spawned object (since spawn is generally not used as a return so ye idk)

    // Start is called before the first frame update
    void Start()
    {
        //floorObj = transform.Find("Floor").gameObject;
        //floorObj.transform.localScale = (0.2f + mapHeight / 5f) * new Vector3(1, 1, 1);
        //FOWfloorObj = transform.Find("FOWFloor").gameObject;
        FOWfloorObj.transform.localScale = (0.2f + mapHeight / 5f) * new Vector3(1, 1, 1);

        //mapCam = GameObject.Find("minimapCamera");
        mapCam.GetComponent<Camera>().orthographicSize = mapHeight + 1;
        //FOWCam1 = GameObject.Find("currentFogPosCam");
        FOWCam1.GetComponent<Camera>().orthographicSize = mapHeight + 1;
        //FOWCam2 = GameObject.Find("allFogCam");
        FOWCam2.GetComponent<Camera>().orthographicSize = mapHeight + 1;

        GameObject upperWall = Instantiate(outerWallObj, new Vector3(0, mapHeight + 24, 0), Quaternion.identity);
        GameObject lowerWall = Instantiate(outerWallObj, new Vector3(0, - mapHeight - 24, 0), Quaternion.identity);
        GameObject rightWall = Instantiate(outerWallObj, new Vector3(mapHeight + 24, 0, 0), Quaternion.Euler(0, 0, 90));
        GameObject LeftWall = Instantiate(outerWallObj, new Vector3( - mapHeight - 24, 0, 0), Quaternion.Euler(0, 0, 90));


        Setup();
        SetRooms();

        foreach (GameObject player in EntityReferencerGuy.Instance.master.GetComponent<playerManagement>().players)
        {
            player.transform.position = MapSpaceToWorldSpace(roomPositions[0]);
        }

        SetWalkers();
        CreateFloors();
        CreateFiller(); // Checks for large enough areas of just plain floors, and rolls to add new obstacles there (prolly just rocks 99% of the time)
        SpawnLevel();
    }

    void Setup()
    {
        grid = new gridSpace[mapHeight, mapHeight];
        gridPosOwners = new int[mapHeight, mapHeight];
        for (int x = 0; x < mapHeight - 1; x++)
        {
            for (int y = 0; y < mapHeight - 1; y++)
            {
                grid[x, y] = gridSpace.wall;
            }
        }

        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                gridPosOwners[x, y] = -5;
            }
        }

        playerSpawnPos = MapSpaceToWorldSpace(new Vector2(Mathf.RoundToInt(mapHeight / 2f), Mathf.RoundToInt(mapHeight / 2f)));

        walkers = new List<walker>();

        //for (int i = 0; i < 3; i++)
        //{
        //    walker newWalker = new walker();
        //    newWalker.dir = RandomDirection();
        //    Vector2 spawnPos = new Vector2(Mathf.RoundToInt(mapHeight / 2f), Mathf.RoundToInt(mapHeight / 2f));
        //    newWalker.pos = spawnPos;
        //    walkers.Add(newWalker);
        //}
    }

    // Create the rooms.
    void SetRooms()
    {
        int numRooms = Random.Range(numRoomsMean - numRoomsRange, numRoomsMean + numRoomsRange);
        for (int i = 0; i < numRooms; i++)
        {
            int roomHeight = Random.Range(roomHeightMin, roomHeightMax);
            int roomWidth = Random.Range(roomWidthMin, roomWidthMax);
            int roomXPos = Random.Range(Mathf.RoundToInt(mapHeight / 8), Mathf.RoundToInt(mapHeight - mapHeight / 8));
            int roomYPos = Random.Range(Mathf.RoundToInt(mapHeight / 8), Mathf.RoundToInt(mapHeight - mapHeight / 8));
            Vector2 roomPos = new Vector2(roomXPos, roomYPos);

            bool positionIsOkay = false;
            int iterationers = 0;
            while (!positionIsOkay)
            {
                float smallestDistance = 500.5f;
                for (int k = 0; k < roomPositions.Count; k++)
                {
                    float magnitude = (roomPos - roomPositions[k]).magnitude;

                    if (magnitude < smallestDistance)
                    {
                        smallestDistance = (roomPos - roomPositions[k]).magnitude;
                    }
                }

                if (smallestDistance < (30 * (1 - iterationers * 0.005f)))
                {
                    roomHeight = Random.Range(roomHeightMin, roomHeightMax);
                    roomWidth = Random.Range(roomWidthMin, roomWidthMax);
                    roomXPos = Random.Range(Mathf.RoundToInt(mapHeight / 8), Mathf.RoundToInt(mapHeight - mapHeight / 8));
                    roomYPos = Random.Range(Mathf.RoundToInt(mapHeight / 8), Mathf.RoundToInt(mapHeight - mapHeight / 8));
                    roomPos = new Vector2(roomXPos, roomYPos);
                }
                else
                {
                    positionIsOkay = true;
                }

                iterationers++;
            }

            CreateRoom(0, roomPos);
            roomPositions.Add(roomPos);
            roomSizes.Add(new Vector2(roomWidth, roomHeight));
            roomsJoined.Add(false);
            roomJoinResponsibles.Add(-5);
        }

        for (int i = 0; i < roomPositions.Count; i++)
        {
            float roomXPos = roomPositions[i].x;
            float roomYPos = roomPositions[i].y;
            float roomHeight = roomSizes[i].y;
            float roomWidth = roomSizes[i].x;

            for (int x = Mathf.FloorToInt(roomXPos) - Mathf.FloorToInt(roomWidth / 2f); x < Mathf.FloorToInt(roomXPos) + Mathf.FloorToInt(roomWidth / 2f); x++)
            {
                for (int y = Mathf.FloorToInt(roomYPos) - Mathf.FloorToInt(roomHeight / 2f); y < Mathf.FloorToInt(roomYPos) + Mathf.FloorToInt(roomHeight / 2f); y++)
                {
                    gridPosOwners[x, y] = i;
                }
            }
        }
    }

    void CreateRoom(int ID, Vector2 position)
    {
        // nothing, I guess here it will spawn the necessary objects for the room specified in the ID.
    }

    // Set the walkers' positions based on the rooms.
    void SetWalkers()
    {
        int i = 0;
        int roomNo = 0;

        //roomNo = i;// Mathf.FloorToInt(i / 4);
        //walker newWalker = new walker();
        //newWalker.dir = RandomDirection();
        //newWalker.pos = new Vector2(Mathf.RoundToInt(mapHeight / 2), Mathf.RoundToInt(mapHeight / 2));//cornerPos;
        //newWalker.origin = roomNo;
        //newWalker.hasJoined = false;
        //Debug.Log("origin: " + newWalker.origin.ToString());
        //walkers.Add(newWalker);
        //i++;

        foreach (Vector2 cornerPos in roomPositions)
        {
            roomNo = i;// Mathf.FloorToInt(i / 4);
            walker newWalker = new walker();
            newWalker.dir = RandomDirection();
            newWalker.pos = cornerPos;
            newWalker.origin = roomNo;
            newWalker.size = 2;
            newWalker.hasJoined = false;
            //Debug.Log("origin: " + newWalker.origin.ToString());
            walkers.Add(newWalker);
            i++;
        }
    }

    void MarkWalkerForRemoval(walker toRemove)
    {
        toRemove.hasJoined = true;
    }

    void CreateFloors()
    {
        int iterations = 0;
        do
        {
            int j = 0;
            foreach (walker myWalker in walkers)
            {
                // If the walker moves onto a space whose room origin isn't this walker's origin. How to prevent things like rooms 1 and 2 connecting, rooms 3 and 4 connecting (so you can't get from 4 to 1)?
                if (gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y] != myWalker.origin && gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y] != -5 && !roomsJoined[myWalker.origin])
                {
                    roomsJoined[myWalker.origin] = true;
                    //roomsJoined[gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y]] = true;
                    //Debug.Log("wow cool this is the joining of isaac: " + ((int)myWalker.origin).ToString());
                    MarkWalkerForRemoval(myWalker);
                    roomJoinResponsibles[gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y]] = myWalker.origin;
                    //Debug.Log("Origin: " + myWalker.origin.ToString() + " / position origin: " + gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y].ToString() + " / position: " + myWalker.pos.ToString());
                }
                //Debug.Log("walker pos: " + (myWalker.pos).ToString());

                if (grid[(int)myWalker.pos.x, (int)myWalker.pos.y] != gridSpace.room)
                {
                    gridPosOwners[(int)myWalker.pos.x, (int)myWalker.pos.y] = myWalker.origin;
                    grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = gridSpace.floor;
                    grid[(int)myWalker.pos.x + 1, (int)myWalker.pos.y] = gridSpace.floor;
                    grid[(int)myWalker.pos.x - 1, (int)myWalker.pos.y] = gridSpace.floor;
                    grid[(int)myWalker.pos.x, (int)myWalker.pos.y + 1] = gridSpace.floor;
                    grid[(int)myWalker.pos.x, (int)myWalker.pos.y - 1] = gridSpace.floor;
                    if (myWalker.size == 3)
                    {
                        grid[(int)myWalker.pos.x, (int)myWalker.pos.y + 2] = gridSpace.floor;
                        grid[(int)myWalker.pos.x + 1, (int)myWalker.pos.y + 1] = gridSpace.floor;
                        grid[(int)myWalker.pos.x + 2, (int)myWalker.pos.y] = gridSpace.floor;
                        grid[(int)myWalker.pos.x + 1, (int)myWalker.pos.y - 1] = gridSpace.floor;
                        grid[(int)myWalker.pos.x, (int)myWalker.pos.y - 2] = gridSpace.floor;
                        grid[(int)myWalker.pos.x - 1, (int)myWalker.pos.y - 1] = gridSpace.floor;
                        grid[(int)myWalker.pos.x - 2, (int)myWalker.pos.y] = gridSpace.floor;
                        grid[(int)myWalker.pos.x - 1, (int)myWalker.pos.y + 1] = gridSpace.floor;

                    }
                }
            }

            // Killing walker
            int numberChecks = walkers.Count;
            for (int i = 0; i < numberChecks; i++)
            {
                walker thisWalker = walkers[i];
                if (((Random.value < chanceWalkerDestroy && walkers.Count > 1) && roomsJoined[thisWalker.origin]) || thisWalker.hasJoined) // The latter is so the walker dies if it has just joined.
                {
                    walkers.RemoveAt(i);
                    break;
                }
            }

            // Changing direction
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir && grid[Mathf.RoundToInt(walkers[i].pos.x), Mathf.RoundToInt(walkers[i].pos.y)] != gridSpace.room) // Only changes direction if it ISN'T currently in a room.
                {
                    walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    //int minSize = Mathf.CeilToInt((thisWalker.dir).magnitude); // To make it so if it's moving diagonally it can't have a size of 1.
                    //thisWalker.size = Mathf.Clamp(thisWalker.size, minSize - 1, 3);
                    //Debug.Log("dir change size moment. walker size: " + thisWalker.size.ToString() + " / min size: " + minSize.ToString() + " / dir: " + thisWalker.dir.ToString());
                    walkers[i] = thisWalker;
                }
            }

            // Changing size
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeSize)
                {
                    walker thisWalker = walkers[i];
                    int newSize = Random.Range(2, 4); // between 2 and 3.
                    thisWalker.size = newSize;
                    //Debug.Log("walker size: " + thisWalker.size.ToString());
                    walkers[i] = thisWalker;
                }
            }

            // Spawning walker
            numberChecks = walkers.Count;
            for (int i = 0; i < numberChecks; i++)
            {
                if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers)
                {
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    newWalker.origin = walkers[i].origin;
                    newWalker.hasJoined = false;
                    //Debug.Log("new walker origin: " + newWalker.origin.ToString());
                    walkers.Add(newWalker);
                }
            }

            // move walkers
            for (int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                walkers[i] = thisWalker;
            }

            // stop walkers from going off map, turning them around if so.
            for (int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 3, mapHeight - 5);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 3, mapHeight - 5);
                //if (thisWalker.pos.x > mapHeight - 2 || thisWalker.pos.x < 1)
                //{
                //    thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, mapHeight - 2);
                //    //thisWalker.dir.x *= -1;
                //}
                //if (thisWalker.pos.y > mapHeight - 2 || thisWalker.pos.y < 1)
                //{
                //    thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, mapHeight - 2);
                //    //thisWalker.dir.y *= -1;
                //}
                walkers[i] = thisWalker;
            }

            // exit loop
            if (percentCompletion() > percentToFill)
            {
                // Checks if all rooms have been joined or not, if so, it breaks out of the loop.
                bool allRoomsJoined = true;
                foreach (bool roomStatus in roomsJoined)
                {
                    if (!roomStatus)
                    {
                        allRoomsJoined = false;
                    }
                }

                if (allRoomsJoined)
                {
                    break;
                }
            }
            iterations++;
            //Debug.Log("iterated");
        }

        while (iterations < 10000);
    }

    void CreateFiller()
    {
        for (int x = 0 + 8; x < mapHeight - 8; x++)
        {
            for (int y = 0 + 8; y < mapHeight - 8; y++)
            {

                // checks all the blocks around the current block to see if they're all free.
                bool spaceIsFree = true;
                for (int x2 = x - 3; x2 < x + 4; x2++)
                {
                    for (int y2 = y - 3; y2 < y + 4; y2++)
                    {
                        if (grid[x2, y2] == gridSpace.wall || grid[x2, y2] == gridSpace.room)
                        {
                            spaceIsFree = false;
                            break; // End the loop early if there's a wall or room (speeds the process up)
                        }
                    }
                }

                if (Random.value < chanceSpawnFiller && spaceIsFree)
                {
                    int typeOfObst = Random.Range(0, 6); // Probably replace this with a kinda hand-made thing, like for the rooms.
                    switch (typeOfObst)
                    {
                        case 0:
                            grid[x, y] = gridSpace.wall;
                            grid[x + 1, y] = gridSpace.wall;
                            grid[x - 1, y] = gridSpace.wall;
                            grid[x, y + 1] = gridSpace.wall;
                            grid[x, y - 1] = gridSpace.wall;
                            break;
                        case 1:
                            grid[x + 1, y] = gridSpace.wall;
                            grid[x - 1, y] = gridSpace.wall;
                            break;
                        case 2:
                            grid[x, y + 1] = gridSpace.wall;
                            grid[x, y - 1] = gridSpace.wall;
                            break;
                        case 3:
                            grid[x + 1, y] = gridSpace.wall;
                            grid[x - 1, y] = gridSpace.wall;
                            grid[x + 1, y + 1] = gridSpace.wall;
                            grid[x + 2, y] = gridSpace.wall;
                            grid[x + 1, y - 1] = gridSpace.wall;
                            grid[x - 1, y - 1] = gridSpace.wall;
                            grid[x - 2, y] = gridSpace.wall;
                            grid[x - 1, y + 1] = gridSpace.wall;
                            break;
                        case 4:
                            grid[x, y + 1] = gridSpace.wall;
                            grid[x, y - 1] = gridSpace.wall;
                            grid[x, y + 2] = gridSpace.wall;
                            grid[x + 1, y + 1] = gridSpace.wall;
                            grid[x + 1, y - 1] = gridSpace.wall;
                            grid[x, y - 2] = gridSpace.wall;
                            grid[x - 1, y - 1] = gridSpace.wall;
                            grid[x - 1, y + 1] = gridSpace.wall;
                            break;
                        case 5:
                            grid[x, y] = gridSpace.wall;
                            grid[x + 1, y] = gridSpace.wall;
                            grid[x - 1, y] = gridSpace.wall;
                            grid[x, y + 1] = gridSpace.wall;
                            grid[x, y - 1] = gridSpace.wall;
                            grid[x, y + 2] = gridSpace.wall;
                            grid[x + 1, y + 1] = gridSpace.wall;
                            grid[x + 2, y] = gridSpace.wall;
                            grid[x + 1, y - 1] = gridSpace.wall;
                            grid[x, y - 2] = gridSpace.wall;
                            grid[x - 1, y - 1] = gridSpace.wall;
                            grid[x - 2, y] = gridSpace.wall;
                            grid[x - 1, y + 1] = gridSpace.wall;
                            break;
                    }
                }
            }
        }
    }

    void SpawnLevel()
    {
        // Clears the walls and blocks in the room's space.
        for (int i = 0; i < roomPositions.Count; i++)
        {
            float roomXPos = roomPositions[i].x;
            float roomYPos = roomPositions[i].y;
            float roomHeight = roomSizes[i].y;
            float roomWidth = roomSizes[i].x;

            for (int x = Mathf.FloorToInt(roomXPos) - Mathf.FloorToInt(roomWidth / 2f); x < Mathf.FloorToInt(roomXPos) + Mathf.FloorToInt(roomWidth / 2f); x++)
            {
                for (int y = Mathf.FloorToInt(roomYPos) - Mathf.FloorToInt(roomHeight / 2f); y < Mathf.FloorToInt(roomYPos) + Mathf.FloorToInt(roomHeight / 2f); y++)
                {
                    grid[x, y] = gridSpace.room;
                }
            }
        }

        for (int x = 0; x < mapHeight - 1; x++)
        {
            for (int y = 0; y < mapHeight - 1; y++)
            {
                switch (grid[x, y])
                {
                    case gridSpace.wall:
                        Spawn(x, y, wallObj);
                        break;
                    case gridSpace.room:
                        Spawn(x, y, roomVisObj);
                        break;
                    //case gridSpace.floor:
                    //    Spawn(x, y, pathObj);
                    //    break;
                }
            }
        }
    }

    void Spawn(int x, int y, GameObject toSpawn)
    {
        Vector3 spawnPos = new Vector3(2 * x - mapHeight + 2, 2 * y - mapHeight + 2);//MapSpaceToWorldSpace(new Vector2(x, y));
        GameObject spawnedThing = Instantiate(toSpawn, spawnPos, Quaternion.identity);
        spawnedThing.transform.SetParent(gameObject.transform);
        spawnedObject = spawnedThing;
    }

    float percentCompletion()
    {
        return (float)NumberOfFloors() / (float)grid.Length;
    }

    Vector2 MapSpaceToWorldSpace(Vector2 mapCoords)
    {
        return new Vector2(2 * mapCoords.x - mapHeight + 2, 2 * mapCoords.y - mapHeight + 2);
    }

    int NumberOfFloors()
    {
        int count = 0;
        foreach (gridSpace space in grid)
        {
            if (space == gridSpace.floor)
            {
                count++;
            }
        }

        return count;
    }

    Vector2 RandomDirection()
    {
        int choice = Mathf.FloorToInt(Random.value * 7.999f);
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            case 4:
                return new Vector2(1, 1);
            case 5:
                return new Vector2(-1, 1);
            case 6:
                return new Vector2(1, -1);
            default:
                return new Vector2(-1, -1);
        }
    }
}
