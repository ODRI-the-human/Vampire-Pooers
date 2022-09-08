using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public delegate void OnKill(Vector3 pos);
    public static OnKill DeathEffects;
}