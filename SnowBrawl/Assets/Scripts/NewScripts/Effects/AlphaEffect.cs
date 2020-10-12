using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaEffect : Effect<float>
{
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void ApplyEffect()
    {
        canvasGroup.alpha = GetNextValue();
    }

    private float GetNextValue()
    {
        float nextValue = Mathf.Lerp(startValue, targetValue, GetCurveValue());

        return nextValue;
    }

    protected override void Reset()
    {
        base.Reset();

        canvasGroup.alpha = startValue;
    }
}
