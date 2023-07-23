using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomParams", menuName = "RoomShit/RoomParams")]
public class roomParams : ScriptableObject
{
    public string roomName = "none";
    public int roomHeight = 12;
    public int roomWidth = 12;
    public int roomWeight = 100; // Likelihood of room to spawn.
    public GameObject roomPrefab;
}
