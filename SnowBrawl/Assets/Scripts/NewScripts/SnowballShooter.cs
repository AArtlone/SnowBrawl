using System;
using System.Collections;
using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    [SerializeField] private Snowball snowballPrefab;
    [SerializeField] private Transform shootingPoint;

    private const float shootingAnimDuration = .25f;

    private float speed = 20f; //TODO: move this somewhere

    public void Shoot(bool facingRight, PlayerID playerID, Action doneShootingCallback)
    {
        StartCoroutine(ShootCo(facingRight, playerID, doneShootingCallback));
    }

    private IEnumerator ShootCo(bool facingRight, PlayerID playerID, Action doneShootingCallback)
    {
        // We need to wait for the animation to finish before the showball is instantiated
        yield return new WaitForSeconds(shootingAnimDuration);

        Snowball snowball = Instantiate(snowballPrefab, shootingPoint.position, Quaternion.identity);

        snowball.Shoot(GetShootingDirection(facingRight), speed, playerID);

        if (doneShootingCallback != null)
            doneShootingCallback();
    }

    private Vector2 GetShootingDirection(bool facingRight)
    {
        Vector2 direction;

        if (facingRight)
            direction = transform.right;
        else
            direction = -transform.right;

        return direction;
    }
}
