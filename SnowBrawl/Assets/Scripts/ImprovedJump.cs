using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ImprovedJump : MonoBehaviour 
{
    [SerializeField] float fallMultiplier;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
    }
}
