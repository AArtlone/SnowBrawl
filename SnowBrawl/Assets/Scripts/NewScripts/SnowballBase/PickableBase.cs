using UnityEngine;

public abstract class PickableBase : MonoBehaviour
{
    public bool CanPickUp { get; protected set; }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        player.EnteredPickableBase();
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        player.ExitedPickableBase();
    }
}
