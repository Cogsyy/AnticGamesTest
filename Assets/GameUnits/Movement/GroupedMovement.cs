using UnityEngine;

public class GroupedMovement : MovementBase
{
    protected override void Update()
    {
        if (target != null)
        {
            IUnit closestFriendly = FindClosestGridEnemy(unit);//Find the closest enemy unit (friendly to this unit)

            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

            if (closestFriendly != null)
            {
                Vector2 directionToFriendly = ((Vector2)closestFriendly.GetPosition() - (Vector2)transform.position).normalized;
                direction = (direction + directionToFriendly * 2).normalized;
            }

            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

            Move(newPosition);
        }

        base.Update();
    }
}
