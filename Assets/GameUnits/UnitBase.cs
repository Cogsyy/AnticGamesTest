using System;
using TMPro;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit
{
    [SerializeField] private TMP_Text _unitLabel;

    [NonSerialized] public IUnit flagUnit;//Assigned by the factory

    protected int maxHealth;
    protected int currentHealth;
    protected int damage;
    protected float attackTimeInSeconds;
    protected float attackTimerSeconds;
    protected float distanceToFlagThreshold;

    public Transform currentTarget { get; protected set; }

    public virtual void Initialize(UnitSettings settings)
    {
        maxHealth = settings.health;
        currentHealth = maxHealth;
        damage = settings.damage;
        attackTimeInSeconds = settings.attackTimeInSeconds;
        attackTimerSeconds = attackTimeInSeconds;
        distanceToFlagThreshold = settings.distanceToFlagThreshold;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    protected void SetUnitName(string unitName)
    {
        _unitLabel.text = unitName;
    }

    public virtual void OnReachedTarget(Transform target)
    {
        currentTarget = target;
    }

    public virtual void OnLeftTarget(Transform target)
    {
        currentTarget = null;
    }

    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            MessagesBroker.Instance.SendMessage(MessagingType.UnitDied, this);//Inform everyone who wants to know that a unit died.
        }
    }

    protected virtual void Update()
    {
        if (attackTimerSeconds < attackTimeInSeconds)
        {
            attackTimerSeconds += Time.deltaTime;
        }
        else
        {
            TryDealDamage();
        }
    }

    protected virtual void TryDealDamage()
    {
        if (currentTarget == null)
        {
            return;
        }

        UnitBase unit = currentTarget.GetComponent<UnitBase>();

        if (unit != null)
        {
            attackTimerSeconds = 0;
            unit.DealDamage(damage);
            OnDamageDealt(damage);
        }
    }

    protected virtual void OnDamageDealt(int damageDealt)
    {

    }

    public bool IsIdle()
    {
        //Can add other criteria here if needed
        return currentTarget == null;
    }
}
