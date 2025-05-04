using System.Collections;
using UnityEngine;

public class AntUnit : UnitBase
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public override void Initialize(UnitSettings settings)
    {
        base.Initialize(settings);
        SetUnitName("Ant");
    }

    protected override void OnDamageDealt(int damageDealt)
    {
        base.OnDamageDealt(damageDealt);

        //Simple attack animation
        StartCoroutine(LerpSpriteColor(Color.red, 0.4f));
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
}
