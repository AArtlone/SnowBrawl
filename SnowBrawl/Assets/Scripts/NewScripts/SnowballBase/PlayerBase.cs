using System;
using UnityEngine;

public class PlayerBase : PickableBase
{
    public Action<int> snowballsChanged;

    [SerializeField] private PlayerID playerID;

    public PlayerID PlayerID { get { return playerID; } }

    private int snowballs;
    public int Snowballs {
        get
        {
            return snowballs;
        }
        set
        {
            snowballs = value;
            if (snowballsChanged != null)
                snowballsChanged.Invoke(snowballs);
        } 
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PlayerID == playerID)
            player.EnteredHome(this);
        else
            player.EnteredEnemyBase(this);
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PlayerID == playerID)
            player.ExitedHome();
        else
            player.ExitedEnemyBase();
    }
}
