using SpatialPartitionSystem;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitFactory : MonoBehaviour
{
    [SerializeField] public UnitProperties unitProperties;

    public abstract IUnit CreateUnit(Vector3 position, Grid<List<IUnit>> grid);
}
