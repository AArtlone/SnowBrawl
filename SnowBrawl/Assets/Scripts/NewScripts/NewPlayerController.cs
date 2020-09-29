using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private KeyCode pickUpKey;

    private PlayerMovement playerMovement;
    private PlayerActions playerActions;

    private void Start()
    {
        groundCheck.DisableCollision(col);

        var mvSettings = NewGameManager.Instance.MVSettings;

        playerMovement = new PlayerMovement(mvSettings, rb, groundCheck);

        playerActions = new PlayerActions();
    }

    private void Update()
    {
        playerActions.CheckForActions();
    }

    private void FixedUpdate()
    {
        playerMovement.UpdateVelocity();
    }
}
