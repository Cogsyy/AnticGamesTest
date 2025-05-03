using UnityEngine;
using SpatialPartitionSystem;
using System.Collections.Generic;

public class MovementBase : MonoBehaviour
{
    public Vector3 oldPosition { get; private set; }
    private Grid<List<IUnit>> _grid;
    private IUnit _unit;

    private void Start()
    {
        _unit = GetComponent<IUnit>();
        oldPosition = transform.position;
    }

    public void SetGrid(Grid<List<IUnit>> grid)
    {
        _grid = grid;
    }
    
    protected void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
        
        _grid.Move(_unit, oldPosition);

        oldPosition = transform.position;
    }
}
