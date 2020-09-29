using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Rigidbody2D rbToMove;
    [SerializeField] private Collider2D col;

    private MovementSettings mvSettings;
    private KeyCode jumpKey = KeyCode.W;

    private void Start()
    {
        mvSettings = NewGameManager.Instance.MVSettings;
        
        groundCheck.DisableCollision(col);
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        UpdateHorizontalVelocity();
        UpdateVerticalVelocity();
    }

    private void UpdateHorizontalVelocity()
    {
        float horizontalInput = Input.GetAxis("P1Horizontal");

        if (horizontalInput > 0)
            Flip(true);
        else if (horizontalInput < 0)
            Flip(false);
        if (horizontalInput == 0)
        {
            rbToMove.velocity = new Vector2(0, rbToMove.velocity.y);
            return;
        }

        rbToMove.AddForce(Vector2.right * mvSettings.speed * horizontalInput);

        float horizontalVelocity = rbToMove.velocity.x;

        horizontalVelocity = Mathf.Clamp(horizontalVelocity, mvSettings.minSpeed, mvSettings.maxSpeed);

        rbToMove.velocity = new Vector2(horizontalVelocity, rbToMove.velocity.y);
    }

    private void UpdateVerticalVelocity()
    {
        if (!groundCheck.IsGrounded)
            return;

        if (Input.GetKey(jumpKey))
            rbToMove.velocity = Vector2.up * mvSettings.springAbility;
    }

    private void Flip(bool right)
    {
        player.FacingRight = right;

        if (right)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
