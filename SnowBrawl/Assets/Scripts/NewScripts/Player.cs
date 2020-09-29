using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SnowballShooter snowballShooter;
    [SerializeField] private PlayerID playerID;
    [SerializeField] private Transform shootingPoint;
    public PlayerID PlayerID { get { return playerID; } }

    private bool nearPickableBase;
    private bool nearHome;

    public bool FacingRight { get; set; }

    private Inventory inventory;

    private void Start()
    {
        inventory = new Inventory();
    }

    public void EnteredHome()
    {
        nearHome = true;
        nearPickableBase = true;
    }

    public void ExitedHome()
    {
        nearHome = false;
        nearPickableBase = false;
    }

    public void EnteredPickableBase()
    {
        nearPickableBase = true;
    }

    public void ExitedPickableBase()
    {
        nearPickableBase = false;
    }

    public void PickUp()
    {
        if (!CheckIfCanPickUp())
            return;

        inventory.Snowballs++;

        Debug.Log("picking up");
    }

    public void Throw()
    {
        if (!CheckIfCanThrow())
            return;

        snowballShooter.Shoot(shootingPoint.position, GetShootingDirection());

        inventory.Snowballs--;

        Debug.Log("throwing");
    }

    public void Drop()
    {
        if (!CheckIfCanDrop())
            return;

        inventory.Snowballs--;

        Debug.Log("Dropping");
    }

    private bool CheckIfCanPickUp()
    {
        if (!nearPickableBase)
            return false;

        if (!inventory.IsInventoryFull())
            return true;
        else
            return false;
    }

    private bool CheckIfCanThrow()
    {
        if (inventory.IsInventoryEmpty())
            return false;
        else
            return true;
    }

    private bool CheckIfCanDrop()
    {
        if (!nearHome)
            return false;

        if (inventory.IsInventoryEmpty())
            return false;
        else
            return true;
    }

    private Vector2 GetShootingDirection()
    {
        Vector2 direction;

        if (FacingRight)
            direction = transform.right;
        else
            direction = -transform.right;

        return direction;
    }
}
