using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ImprovedJump : MonoBehaviour 
{
    private Rigidbody2D rb;
    private MovementSettings movementSettings;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        movementSettings = GameManager.Instance.MVSettings;
    }

    public void Update()
    {
        if(rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (movementSettings.fallMultiplier - 1) * Time.deltaTime;
    }
}
