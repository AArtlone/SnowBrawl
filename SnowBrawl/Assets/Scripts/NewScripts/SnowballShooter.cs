using System.Collections;
using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    [SerializeField] private Snowball snowballPrefab;

    private const float shootingAnimDuration = .25f;

    private float speed = 20f;

    public void Shoot(Transform shootPos, Vector2 direction, PlayerID playerID)
    {
        StartCoroutine(ShootCo(shootPos, direction, playerID));
    }

    private IEnumerator ShootCo(Transform shootPos, Vector2 direction, PlayerID playerID)
    {
        // We need to wait for the animation to finish before the showball is instantiated
        yield return new WaitForSeconds(shootingAnimDuration);

        Snowball snowball = Instantiate(snowballPrefab, shootPos.position, Quaternion.identity);

        snowball.Shoot(direction, speed, playerID);
    }
}
