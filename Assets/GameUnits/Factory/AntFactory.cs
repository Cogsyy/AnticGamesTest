using SpatialPartitionSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntFactory : UnitFactory
{
    [SerializeField] private AntUnit antPrefab;

    public override IUnit CreateUnit(Vector3 position, Vector3 flagPosition, Grid<List<IUnit>> grid)
    {
        AntUnit newUnit = Instantiate(antPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.antProperties);

        return newUnit;
    }
}
