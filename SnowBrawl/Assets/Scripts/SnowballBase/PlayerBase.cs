using System;
using UnityEngine;

public class PlayerBase : PickableBase
{
    public Action<int> snowballsChanged;

    [SerializeField] private PlayerID playerID;
    [SerializeField] private Transform canvas;

    public PlayerID PlayerID { get { return playerID; } }
    public Transform Canvas { get { return canvas; } }

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

        player.EnteredPickableBase(this);

        //if (player.PlayerID == playerID)
        //    player.EnteredHome(this);
        //else
        //    player.EnteredPickableBase();
    }

    //protected override void OnTriggerExit2D(Collider2D col)
    //{
    //    base
    //    Player player = col.GetComponent<Player>();

    //    if (player == null)
    //        return;

    //    player.ExitedPickableBase();

    //    //if (player.PlayerID == playerID)
    //    //    player.ExitedHome();
    //    //else
    //    //    player.ExitedPickableBase();
    //}
}
