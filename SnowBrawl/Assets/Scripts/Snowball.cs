using UnityEngine;

public class Snowball : MonoBehaviour 
{
    [SerializeField] private Rigidbody2D rb;

    private PlayerID shooterID;

    public void Shoot(Vector2 direction, float speed, PlayerID shooterID)
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

        // TODO: Kill other player
        NewGameManager.Instance.KillPlayer(player);

        Destroy(gameObject);
    }
}
