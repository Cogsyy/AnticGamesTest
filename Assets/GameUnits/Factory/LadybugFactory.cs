using SpatialPartitionSystem;
using System.Collections.Generic;
using UnityEngine;

public class LadybugFactory : UnitFactory
{
    [SerializeField] private LadybugUnit _ladybugPrefab;
    [SerializeField] private Transform _flag;

    public override IUnit CreateUnit(Vector3 position, Grid<List<IUnit>> grid)
    {
        LadybugUnit newUnit = Instantiate(_ladybugPrefab, position, Quaternion.identity, transform);

        newUnit.Initialize(unitProperties.ladybugProperties);
        newUnit.flagUnit = _flag as IUnit;

        return newUnit;
    }
}
