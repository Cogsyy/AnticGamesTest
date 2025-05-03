using System;
using UnityEngine;

//Demonstrate proper ScriptableObject usage for both design-time and runtime data
//Implement at least one event system for game state communication

[CreateAssetMenu(fileName = "UnitProperties", menuName = "Scriptable Objects/UnitProperties")]
public class UnitProperties : ScriptableObject
{
    public UnitSettings antProperties;
    public UnitSettings aphidProperties;
    public UnitSettings beetleProperties;
    public UnitSettings ladybugProperties;
}

[Serializable]
public struct UnitSettings
{
    public int health;
    public float moveSpeed;
    public int damage;
}
