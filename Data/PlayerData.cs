using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float baseMoveSpeed = 50f;
    public float baseMoveSmoothing = 0.1f;
    public float InteractCooldownTime = 0.33f;
}
