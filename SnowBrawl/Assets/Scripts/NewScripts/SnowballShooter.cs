using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D snowballPrefab;
    private float speed = 20f;

    public void Shoot(Vector3 shootPos, Vector2 direction)
    {
        Rigidbody2D snowball = Instantiate(snowballPrefab, shootPos, Quaternion.identity);

        snowball.velocity = direction * speed;
    }
}
