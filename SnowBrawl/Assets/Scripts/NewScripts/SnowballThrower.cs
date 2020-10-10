using System;
using System.Collections;
using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private Snowball snowballPrefab;
    [SerializeField] private Transform throwingPoint;

    private const float throwingAnimDuration = .25f; //TODO: get animation duration from the anim controller;
    private const float speed = 20f;

    public void Throw(bool facingRight, PlayerID playerID, Action doneShootingCallback)
    {
        StartCoroutine(ThrowCo(facingRight, playerID, doneShootingCallback));
    }

    private IEnumerator ThrowCo(bool facingRight, PlayerID playerID, Action doneShootingCallback)
    {
        // We need to wait for the animation to finish before the showball is instantiated
        yield return new WaitForSeconds(throwingAnimDuration);

        Snowball snowball = Instantiate(snowballPrefab, throwingPoint.position, Quaternion.identity);

        snowball.Throw(GetThrowingDirection(facingRight), speed, playerID);

        if (doneShootingCallback != null)
            doneShootingCallback();
    }

    private Vector2 GetThrowingDirection(bool facingRight)
    {
        Vector2 direction;

        if (facingRight)
            direction = transform.right;
        else
            direction = -transform.right;

        return direction;
    }
}
