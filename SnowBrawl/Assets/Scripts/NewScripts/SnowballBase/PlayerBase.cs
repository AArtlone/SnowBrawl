using UnityEngine;

public class PlayerBase : PickableBase
{
    [SerializeField] private PlayerID playerID;

    public PlayerID PlayerID { get { return playerID; } }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PlayerID == playerID)
            player.EnteredHome();
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PlayerID == playerID)
            player.ExitedHome();
    }
}
