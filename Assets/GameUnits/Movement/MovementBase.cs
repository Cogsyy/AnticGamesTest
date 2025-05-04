using System;
using UnityEngine;
using SpatialPartitionSystem;
using System.Collections.Generic;

public class MovementBase : MonoBehaviour
{
    protected Transform target;
    protected bool reachedTarget;
    public event Action<Transform> OnReachedTarget;
    public event Action<Transform> OnLeftTarget;

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

    public void SetTargetTransform(Transform target)
    {
        this.target = target;
    }

    protected void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
        
        _grid.Move(_unit, oldPosition);

        oldPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 1f)//Stop if close enough
            {
                if (!reachedTarget)
                {
                    reachedTarget = true;
                    OnReachedTarget?.Invoke(target);
                }

                return;
            }
            else
            {
                if (reachedTarget)
                {
                    OnLeftTarget?.Invoke(target);
                    reachedTarget = false;
                }
            }
        }
    }
}
