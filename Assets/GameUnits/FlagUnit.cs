using UnityEngine;

public class FlagUnit : MonoBehaviour, IUnit
{
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Initialize(UnitSettings settings)
    {
        //Unused, Flag is an IUnit so it can be used in calculations on the Grid
    }
}
