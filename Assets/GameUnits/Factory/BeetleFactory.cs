using SpatialPartitionSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeetleFactory : UnitFactory
{
    [SerializeField] private BeetleUnit _beetlePrefab;
    [SerializeField] private Transform _flag;

    public override IUnit CreateUnit(Vector3 position, Grid<List<IUnit>> grid)
    {
        BeetleUnit newUnit = Instantiate(_beetlePrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.beetleProperties);
        newUnit.flagUnit = _flag as IUnit;

        LinearMovement movement = newUnit.AddComponent<LinearMovement>();
        
        movement.SetMoveTarget(_flag);
        movement.SetSpeed(unitProperties.beetleProperties.moveSpeed);
        movement.SetGrid(grid);
        movement.OnReachedTarget += newUnit.OnReachedTarget;

        return newUnit;
    }
}
