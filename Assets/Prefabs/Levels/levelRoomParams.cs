using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomParams", menuName = "RoomShit/LevelAvailParams")]
public class levelRoomParams : ScriptableObject
{
    public string levelToUseIn;
    public roomParams[] roomsInLevel;
}
