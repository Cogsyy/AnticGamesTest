
using UnityEngine;

public class LinearMovement : MovementBase
{
    private float _speed = 1;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    protected override void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * _speed * Time.deltaTime;

            Move(newPosition);
        }

        base.Update();
    }
}
