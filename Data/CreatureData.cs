using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCreatureData", menuName = "Data/Creature Data/Base Data")]

public class CreatureData : ScriptableObject
{
    [Header("Creature Stats")]
    public float baseMoveSpeed = 50f;
    public float baseMoveSmoothing = 0f;
    public float InteractCooldownTime = 0.33f;
    public float baseMoveRange = 3f;
    public float destinationThreshold = 0.1f;
    
}
