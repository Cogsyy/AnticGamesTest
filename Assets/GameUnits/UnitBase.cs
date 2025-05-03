using TMPro;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit
{
    [SerializeField] private TMP_Text _unitLabel;

    protected int maxHealth;
    protected int currentHealth;
    protected int damage;

    public virtual void Initialize(UnitSettings settings)
    {
        maxHealth = settings.health;
        currentHealth = maxHealth;
        damage = settings.damage;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    protected void SetUnitName(string unitName)
    {
        _unitLabel.text = unitName;
    }

    public virtual void OnReachedTarget(Transform target)
    {

    }

    public virtual void OnLeftTarget(Transform target)
    {

    }

    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            MessagesBroker.Instance.SendMessage(MessagingType.UnitDied, this);//Inform everyone who wants to know that a unit died.
        }
    }
}
