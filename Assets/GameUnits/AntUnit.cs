using UnityEngine;

public class AntUnit : UnitBase
{
    public override void Initialize(UnitSettings settings)
    {
        base.Initialize(settings);
        SetUnitName("Ant");
    }

    public override void OnReachedTarget(Transform target)
    {
        base.OnReachedTarget(target);
        //Deal damage to the target
        UnitBase unit = target.GetComponent<UnitBase>();
        if (unit != null)
        {
            unit.DealDamage(damage);
            
        }
    }
}
