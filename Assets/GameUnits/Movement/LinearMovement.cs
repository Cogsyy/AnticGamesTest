using UnityEngine;

public class LinearMovement : MovementBase
{
    private Transform _target;
    private float _speed = 1;

    public void SetTargetTransform(Transform target)
    {
        _target = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * _speed * Time.deltaTime;

            Move(newPosition);
        }
    }
}
