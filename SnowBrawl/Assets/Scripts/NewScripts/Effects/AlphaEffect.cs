using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaEffect : Effect<float>
{
    private CanvasGroup canvasGroup;

    protected override void Smth()
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

        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = startValue;
    }
}
