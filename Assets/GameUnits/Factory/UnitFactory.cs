using SpatialPartitionSystem;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    [SerializeField] protected UnitProperties unitProperties;

    public abstract IUnit CreateUnit(Vector3 position, Vector3 flagPosition, Grid<List<IUnit>> grid);
}
