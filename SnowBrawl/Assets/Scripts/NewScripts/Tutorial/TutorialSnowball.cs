using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TutorialSnowball : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    public void Shoot(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }
}
