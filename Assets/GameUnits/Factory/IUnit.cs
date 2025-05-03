using UnityEngine;

public interface IUnit
{
    void Initialize(UnitSettings settings);
    public Vector2 GetPosition();
}
