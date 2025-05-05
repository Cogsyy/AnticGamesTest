
using UnityEngine;

public class LinearMovement : MovementBase
{
    protected override void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

            Move(newPosition);
        }

        base.Update();
    }
}
