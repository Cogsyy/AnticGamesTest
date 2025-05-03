using SpatialPartitionSystem;
using System.Collections.Generic;
using UnityEngine;

public class LadybugFactory : UnitFactory
{
    [SerializeField] private LadybugUnit ladybugPrefab;

    public override IUnit CreateUnit(Vector3 position, Vector3 flagPosition, Grid<List<IUnit>> grid)
    {
        LadybugUnit newUnit = Instantiate(ladybugPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.ladybugProperties);

        return newUnit;
    }
}
