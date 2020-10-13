using System;
using System.Collections;
using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private Snowball snowballPrefab;
    [SerializeField] private Transform throwingPoint;

    private const float throwingAnimDuration = .25f;
    private const float speed = 20f;
    private const float snowballDestroyDelay = 5f;

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

        yield return new WaitForSeconds(snowballDestroyDelay);

        if (GameManager.Instance.GameIsPaused)
            yield break;

        if (snowball == null)
            yield break;

        Destroy(snowball.gameObject);
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
