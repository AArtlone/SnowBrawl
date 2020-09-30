using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<int> numOfSnowballChanged;

    [SerializeField] private SnowballShooter snowballShooter;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private PlayerID playerID;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private KeysSettings keysSettings;


    public PlayerID PlayerID { get { return playerID; } }
    public KeysSettings KeysSettings { get { return keysSettings; } }

    private bool nearPickableBase;
    private bool nearHome;

    public bool FacingRight { get; set; }

    private Inventory inventory;

    private PlayerBase homeBase;

    private void Start()
    {
        inventory = new Inventory();
    }

    public void EnteredHome(PlayerBase playerBase)
    {
        nearHome = true;
        nearPickableBase = true;

        homeBase = playerBase;
    }

    public void ExitedHome()
    {
        nearHome = false;
        nearPickableBase = false;

        homeBase = null;
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

        RaiseSnowballChangedEvent();

        Debug.Log(playerID + " is picking up");
    }

    public void Throw()
    {
        if (!CheckIfCanThrow())
            return;

        snowballShooter.Shoot(shootingPoint, GetShootingDirection(), playerID);

        animationController.ShootAnimation();

        inventory.Snowballs--;

        RaiseSnowballChangedEvent();

        Debug.Log(playerID + " is throwing");
    }

    public void Drop()
    {
        if (!CheckIfCanDrop())
            return;

        if (homeBase != null)
            homeBase.Snowballs++;

        inventory.Snowballs--;

        RaiseSnowballChangedEvent();

        Debug.Log(playerID + " is dropping");
    }

    public void Kill()
    {
        inventory.Snowballs = 0;

        RaiseSnowballChangedEvent();

        Destroy(gameObject);
    }

    private void RaiseSnowballChangedEvent()
    {
        if (numOfSnowballChanged != null)
            numOfSnowballChanged.Invoke(inventory.Snowballs);
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
