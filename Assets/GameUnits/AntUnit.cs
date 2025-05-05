using System;
using System.Collections;
using UnityEngine;

public class AntUnit : UnitBase
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _isAIMode;
    public string debugReasoning { get; private set; }

    public override void Initialize(UnitSettings settings)
    {
        base.Initialize(settings);
        SetUnitName("Ant");
        _isAIMode = settings.isAIMode;
        MessagesBroker.Instance.AddListener<bool>(MessagingType.AIModeToggled, OnAIModeToggled);
    }

    private void OnDestroy()
    {
        MessagesBroker.Instance.RemoveListener<bool>(MessagingType.AIModeToggled, OnAIModeToggled);
    }

    private void OnAIModeToggled(bool isAIMode)
    {
        _isAIMode = isAIMode;
    }

    protected override void OnDamageDealt(int damageDealt)
    {
        base.OnDamageDealt(damageDealt);

        //Simple attack animation
        StartCoroutine(LerpSpriteColor(Color.red, 0.4f));
    }

    public void SetMoveTarget(Transform target)
    {
        if (target == null)
            return;

        MovementBase movement = GetComponent<MovementBase>();
        if (movement != null)
        {
            movement.SetMoveTarget(target);
        }
    }

    private IEnumerator LerpSpriteColor(Color targetColor, float duration)
    {
        Color startingColor = spriteRenderer.color;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            float sinT = Mathf.Sin(t * Mathf.PI);
            spriteRenderer.color = Color.Lerp(startingColor, targetColor, sinT);
            yield return null;
        }

        spriteRenderer.color = startingColor;
    }

    protected override void Update()
    {
        base.Update();
        if (_isAIMode && IsIdle())
        {
            //Find a new target, pick the closest enemy unit
            MovementBase movement = GetComponent<MovementBase>();
            if (movement != null)
            {
                IUnit nextTarget = null;

                IUnit closestTargetToFlag = movement.FindClosestGridEnemy(flagUnit);
                if (closestTargetToFlag != null)
                {
                    float distanceToFlag = Vector3.Distance(closestTargetToFlag.GetPosition(), flagUnit.GetPosition());
                    if (distanceToFlag < distanceToFlagThreshold)
                    {
                        nextTarget = closestTargetToFlag;
                        debugReasoning = "Targeting an enemy close to the flag, it's closer than my threshold allows at distance of " + distanceToFlag;
                    }
                }

                if (nextTarget == null)
                {
                    IUnit closestTargetToMe = movement.FindClosestGridEnemy(this);
                    nextTarget = closestTargetToMe;
                    debugReasoning = "Targeting an enemy close to me, since no one is too close to threatening the flag yet.";
                }

                if (nextTarget != null)
                {
                    movement.SetMoveTarget(nextTarget.GetTransform());
                }
            }
        }
    }
}
