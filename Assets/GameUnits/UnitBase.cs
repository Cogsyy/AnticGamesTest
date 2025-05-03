using TMPro;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IUnit
{
    [SerializeField] private TMP_Text _unitLabel;

    public virtual void Initialize(UnitSettings settings)
    {
        
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    protected void SetUnitName(string unitName)
    {
        _unitLabel.text = unitName;
    }
}
