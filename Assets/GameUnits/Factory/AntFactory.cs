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

        LinearMovement movement = newUnit.AddComponent<LinearMovement>();

        movement.SetSpeed(unitProperties.antProperties.moveSpeed);
        movement.SetGrid(grid);
        movement.OnReachedTarget += newUnit.OnReachedTarget;
        movement.OnLeftTarget += newUnit.OnLeftTarget;

        return newUnit;
    }
}
