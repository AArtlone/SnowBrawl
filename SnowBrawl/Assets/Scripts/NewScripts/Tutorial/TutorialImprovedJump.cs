using UnityEngine;

public class TutorialImprovedJump : ImprovedJump
{
    [SerializeField] private float fallMultiplierValue;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        fallMultiplier = fallMultiplierValue;
    }

    protected override void Update()
    {
        if (rb.velocity.y < 5)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
}
