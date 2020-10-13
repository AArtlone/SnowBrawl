using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Rigidbody2D rb;
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
        rb.simulated = false;
    }

    private void Update()
    {
        if (GameManager.GameIsPaused)
            return;

        UpdateVerticalVelocity();
    }

    private void FixedUpdate()
    {
        if (GameManager.GameIsPaused)
            return;

        UpdateHorizontalVelocity();
    }

    private void UpdateHorizontalVelocity()
    {
        var horizontalInput = SBInputManager.GetPlayerInput(player.PlayerID);

        if (horizontalInput == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        FlipCharacter(horizontalInput);

        var speed = mvSettings.speed;

        if (player.PowerUpsManager.HasPowerUp(PowerUpType.Boots))
            speed = mvSettings.poweredUpSpeed;

        rb.AddForce(Vector2.right * speed * horizontalInput);

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        var horizontalVelocity = rb.velocity.x;

        horizontalVelocity = Mathf.Clamp(horizontalVelocity, mvSettings.minSpeed, mvSettings.maxSpeed);

        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    private void UpdateVerticalVelocity()
    {
        print(groundCheck.IsGrounded);

        if (!groundCheck.IsGrounded)
            return;

        if (SBInputManager.GetKeyDown(jumpKey))
        {
            rb.velocity = Vector2.up * mvSettings.springAbility;
            SoundManager.PlaySound(Sound.Jump);
        }
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
    private SBInputManager SBInputManager { get { return SBInputManager.Instance; } }
}
