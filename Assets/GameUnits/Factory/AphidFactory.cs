using SpatialPartitionSystem;
using System.Collections.Generic;
using UnityEngine;

public class AphidFactory : UnitFactory
{
    [SerializeField] private AphidUnit aphidPrefab;

    public override IUnit CreateUnit(Vector3 position, Vector3 flagPosition, Grid<List<IUnit>> grid)
    {
        AphidUnit newUnit = Instantiate(aphidPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.aphidProperties);

        return newUnit;
    }
}
