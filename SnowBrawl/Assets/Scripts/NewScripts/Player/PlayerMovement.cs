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
    }

    private void OnDestroy()
    {
        GameManager.onRoundOver -= OnRoundOver;
    }

    private void Start()
    {
        mvSettings = GameManager.MVSettings;

        jumpKey = player.KeysSettings.jumpKey;
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
        var horizontalInput = SBInputManager.Instance.GetPlayerInput(player.PlayerID);

        if (horizontalInput == 0)
        {
            rbToMove.velocity = new Vector2(0, rbToMove.velocity.y);
            return;
        }

        FlipCharacter(horizontalInput);

        var speed = mvSettings.speed;

        if (player.PowerUpsManager.HasPowerUp(PowerUpType.Boots))
            speed = mvSettings.poweredUpSpeed;

        rbToMove.AddForce(Vector2.right * speed * horizontalInput);

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        var horizontalVelocity = rbToMove.velocity.x;

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
