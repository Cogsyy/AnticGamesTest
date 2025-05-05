using SpatialPartitionSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntFactory : UnitFactory
{
    [SerializeField] private AntUnit _antPrefab;
    [SerializeField] private Transform _flag;
    [SerializeField] private DebugPanel _debugPanel;

    public override IUnit CreateUnit(Vector3 position, Grid<List<IUnit>> grid)
    {
        AntUnit newUnit = Instantiate(_antPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.antProperties);
        newUnit.flagUnit = _flag.GetComponent<IUnit>();

        LinearMovement movement = newUnit.AddComponent<LinearMovement>();

        movement.SetSpeed(unitProperties.antProperties.moveSpeed);
        movement.SetGrid(grid);
        movement.OnReachedTarget += newUnit.OnReachedTarget;
        movement.OnLeftTarget += newUnit.OnLeftTarget;

        _debugPanel.RegisterAnt(newUnit);

        return newUnit;
    }
}
