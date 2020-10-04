using UnityEngine;

public class PositionEffect : Effect<Vector3>
{
    protected override void ApplyEffect()
    {
        transform.localPosition = GetNextValue();
    }

    private Vector3 GetNextValue()
    {
        Vector3 nextValue = Vector3.Lerp(startValue, targetValue, GetCurveValue());

        return nextValue;
    }

    protected override void Reset()
    {
        base.Reset();

        transform.localPosition = startValue;
    }
}
