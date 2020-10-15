using UnityEngine;

public class Snowball : MonoBehaviour 
{
    [SerializeField] private Rigidbody2D rb;

    private PlayerID shooterID;

    private void Awake()
    {
        GameManager.onRoundOver += OnRoundOver;
    }

    private void OnDestroy()
    {
        GameManager.onRoundOver -= OnRoundOver;
    }

    private void OnRoundOver()
    {
        rb.simulated = false;
    }

    public void Throw(Vector2 direction, float speed, PlayerID shooterID)
    {
        rb.velocity = direction * speed;

        this.shooterID = shooterID;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            return;
        }

        CheckIfHitOtherPlayer(col);
    }

    private void CheckIfHitOtherPlayer(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PlayerID == shooterID)
            return;

        GameManager.KillPlayer(player, true);

        Destroy(gameObject);
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
