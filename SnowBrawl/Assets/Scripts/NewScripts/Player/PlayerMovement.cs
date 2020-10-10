using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Rigidbody2D rbToMove;
    [SerializeField] private Collider2D col;

    private MovementSettings mvSettings;
    private KeyCode jumpKey;

    private void Awake()
    {
        GameManager.onRoundOver += OnRoundOver;

        jumpKey = player.KeysSettings.jumpKey;
    }

    private void Start()
    {
        mvSettings = GameManager.MVSettings;
    }
    private void OnRoundOver()
    {
        rbToMove.simulated = false;
    }

    private void FixedUpdate()
    {
        if (GameManager.GameIsPaused)
            return;

        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        UpdateHorizontalVelocity();
        UpdateVerticalVelocity();
    }

    private void UpdateHorizontalVelocity()
    {
        string id = player.PlayerID.ToString();

        float horizontalInput = Input.GetAxisRaw(id + " Keyboard");

        if (horizontalInput == 0)
            horizontalInput = Input.GetAxisRaw(id);

        if (horizontalInput == 0)
        {
            rbToMove.velocity = new Vector2(0, rbToMove.velocity.y);
            return;
        }

        FlipCharacter(horizontalInput);

        float speed = mvSettings.speed;

        if (player.PowerUpsManager.HasPowerUp(PowerUpType.Boots))
            speed = mvSettings.poweredUpSpeed;

        rbToMove.AddForce(Vector2.right * speed * horizontalInput);

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

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            player.FacingRight = true;
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            player.FacingRight = false;

            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
