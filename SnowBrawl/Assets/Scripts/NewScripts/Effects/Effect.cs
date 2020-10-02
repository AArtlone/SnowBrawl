using System.Collections;
using UnityEngine;

public abstract class Effect<T> : MonoBehaviour
{
    public T startValue;

    public T targetValue;

    public bool playOnStart;

    public Tween tween;

    private bool waitingForStartDelay;
    private bool playEffect;
    private bool reverse;

    private float normTime;
    private float playTime;

    private float delayTimeLeft;

    private void Awake()
    {
        Reset();
    }

    private void Start()
    {
        if (playOnStart)
            PlayEffect();
    }

    private void Update()
    {
        if (!playEffect)
            return;

        if (waitingForStartDelay)
        {
            UpdateDelayTime();
            return;
        }

        UpdateEffectTime();

        Smth();
    }

    private void UpdateEffectTime()
    {
        if (reverse)
        {
            playTime -= Time.deltaTime;

            normTime = playTime / tween.targetTime;

            if (normTime <= 0)
                EffectFinished();
        }
        else
        {
            playTime += Time.deltaTime;

            normTime = playTime / tween.targetTime;

            if (normTime >= 1)
                EffectFinished();
        }
    }

    private void UpdateDelayTime()
    {
        delayTimeLeft -= Time.deltaTime;

        if (delayTimeLeft <= 0)
            waitingForStartDelay = false;
    }

    private void EffectFinished()
    {
        switch (tween.playStyle)
        {
            case TweenPlayStyle.Once:
                playEffect = false;
                break;
            case TweenPlayStyle.Repeat:
                Reset();
                break;
            case TweenPlayStyle.PingPong:
                reverse = !reverse;
                break;
        }
    }

    protected float GetCurveValue()
    {
        return tween.curve.Evaluate(playTime);
    }

    public void PlayEffect()
    {
        playEffect = true;
    }

    protected virtual void Reset()
    {
        playTime = 0f;

        if (tween.NeedsDelay)
        {
            waitingForStartDelay = true;
            delayTimeLeft = tween.delay;
        }
    }

    protected abstract void Smth();
}
