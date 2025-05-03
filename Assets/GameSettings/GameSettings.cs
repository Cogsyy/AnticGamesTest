using UnityEngine;

using System;
//Demonstrate proper ScriptableObject usage for both design-time and runtime data
//Implement at least one event system for game state communication

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
public class GameSettings : ScriptableObject
{
    //Spawn rates, difficulty settings
    public int enemyCount = 20;

    public GridSettings gridSettings;

}

[Serializable]
public class GridSettings
{
    //Designed as a const, don't modify dynamically. Marking as readonly won't serialize this as a field
    public int MIN_WORLD_X = -20;
    public int MIN_WORLD_Y = -20;

    public int gridWidth;
    public int cellSize;
}
