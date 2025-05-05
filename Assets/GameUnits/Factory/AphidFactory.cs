using SpatialPartitionSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AphidFactory : UnitFactory
{
    [SerializeField] private AphidUnit _aphidPrefab;
    [SerializeField] private Transform _flag;

    public override IUnit CreateUnit(Vector3 position, Grid<List<IUnit>> grid)
    {
        AphidUnit newUnit = Instantiate(_aphidPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.aphidProperties);
        newUnit.flagUnit = _flag as IUnit;

        GroupedMovement movement = newUnit.AddComponent<GroupedMovement>();

        movement.SetMoveTarget(_flag);
        movement.SetSpeed(unitProperties.aphidProperties.moveSpeed);
        movement.SetGrid(grid);
        movement.OnReachedTarget += newUnit.OnReachedTarget;

        return newUnit;
    }
}
