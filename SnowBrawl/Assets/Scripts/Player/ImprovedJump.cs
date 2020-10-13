using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ImprovedJump : MonoBehaviour 
{
    protected Rigidbody2D rb;

    protected float fallMultiplier;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        fallMultiplier = GameManager.Instance.MVSettings.fallMultiplier;
    }

    protected virtual void Update()
    {
        if(rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
}
