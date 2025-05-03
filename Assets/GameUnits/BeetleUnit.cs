using UnityEngine;

public class BeetleUnit : UnitBase
{
    public override void Initialize(UnitSettings settings)
    {
        base.Initialize(settings);
        SetUnitName("Beetle");
    }

    public override void OnReachedTarget(Transform target)
    {
        base.OnReachedTarget(target);
        MessagesBroker.Instance.SendMessage(MessagingType.EnemyReachedFlag);//Inform everyone who wants to know that an enemy reached the flag.
    }
}
