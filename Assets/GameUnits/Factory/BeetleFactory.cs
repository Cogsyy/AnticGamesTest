using SpatialPartitionSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeetleFactory : UnitFactory
{
    [SerializeField] private BeetleUnit beetlePrefab;
    [SerializeField] private Transform flagTransform;

    public override IUnit CreateUnit(Vector3 position, Vector3 flagPosition, Grid<List<IUnit>> grid)
    {
        BeetleUnit newUnit = Instantiate(beetlePrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.beetleProperties);
        LinearMovement movement = newUnit.AddComponent<LinearMovement>();
        
        movement.SetTargetTransform(flagTransform);
        movement.SetSpeed(unitProperties.beetleProperties.moveSpeed);
        movement.SetGrid(grid);

        return newUnit;
    }
}
