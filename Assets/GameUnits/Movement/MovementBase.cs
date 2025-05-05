using System;
using UnityEngine;
using SpatialPartitionSystem;
using System.Collections.Generic;

public class MovementBase : MonoBehaviour
{
    protected const float CLOSE_ENOUGH = 0.25f;

    protected Transform target;
    protected bool reachedTarget;
    public event Action<Transform> OnReachedTarget;
    public event Action<Transform> OnLeftTarget;

    public Vector3 oldPosition { get; private set; }
    private Grid<List<IUnit>> _grid;
    protected IUnit unit;
    protected float currentDistanceToTarget;

    protected float speed = 1;

    private void Start()
    {
        unit = GetComponent<IUnit>();
        oldPosition = transform.position;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetGrid(Grid<List<IUnit>> grid)
    {
        _grid = grid;
    }

    public void SetMoveTarget(Transform target)
    {
        this.target = target;
    }

    public virtual IUnit FindClosestGridEnemy(IUnit comparingUnit)
    {
        return _grid.FindClosestUnitTo(comparingUnit, friendly:false);
    }

    protected void Move(Vector3 newPosition)
    {
        if (currentDistanceToTarget < CLOSE_ENOUGH)
        {
            return;
        }

        transform.position = newPosition;
        
        _grid.Move(unit, oldPosition);

        oldPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            currentDistanceToTarget = Vector3.Distance(transform.position, target.position);
            if (currentDistanceToTarget < CLOSE_ENOUGH)//Stop if close enough
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
