using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    public abstract IUnit GetProduct(Vector3 position);
}
