using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    [SerializeField] private Snowball snowballPrefab;
    private float speed = 20f;

    public void Shoot(Vector3 shootPos, Vector2 direction, PlayerID playerID)
    {
        Snowball snowball = Instantiate(snowballPrefab, shootPos, Quaternion.identity);

        snowball.Shoot(direction, speed, playerID);
    }
}
