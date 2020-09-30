using System;
using UnityEngine;

public class PlayerActions: MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private SnowballShooter snowballShooter;

    [SerializeField] private PlayerAnimationController animationController;

    private const float timeToPickUp = .5f; // Time that the pickUpKey must be pressed to trigger pick up

    private KeyCode pickUpKey;
    private KeyCode throwKey;
    private KeyCode dropKey;

    private float pickUpKeyDownTime;

    private bool isShooting;

    private void Awake()
    {
        pickUpKey = player.KeysSettings.pickUpKey;
        throwKey = player.KeysSettings.throwKey;
        dropKey = player.KeysSettings.dropKey;
    }

    private void Update()
    {
        CheckForActions();
    }

    public void CheckForActions()
    {
        CheckForPickUp();
        
        if (Input.GetKeyDown(throwKey))
            Shoot();
        
        if (Input.GetKeyDown(dropKey))
            Drop();
    }

    private void CheckForPickUp()
    {
        bool pickUpKeyIsDown = Input.GetKey(pickUpKey);

        // If the pickUpKey is pressed, incerement the key down timer and check if it is higher than time needed
        if (pickUpKeyIsDown)
        {
            if (!CheckIfCanPickUp())
                return;

            animationController.StartPickUpAnimation();

            pickUpKeyDownTime += Time.deltaTime;

            if (pickUpKeyDownTime < timeToPickUp)
                return;

            PickUp();

            pickUpKeyDownTime = 0;

            // If inventory is full then we stop the animation
            if (player.Inventory.IsInventoryFull())
                animationController.StopPickUpAnimation();
        }
        else
        {
            animationController.StopPickUpAnimation();
            pickUpKeyDownTime = 0;
        }
    }

    public void PickUp()
    {
        player.Inventory.Snowballs++;

        player.RaiseSnowballChangedEvent();
    }

    public void Shoot()
    {
        if (!CheckIfCanShoot())
            return;

        isShooting = true;

        var doneShootingCallback = new Action(() =>
        {
            isShooting = false;
        });

        snowballShooter.Shoot(player.FacingRight, player.PlayerID, doneShootingCallback);

        animationController.ShootAnimation();

        player.Inventory.Snowballs--;

        player.RaiseSnowballChangedEvent();
    }

    public void Drop()
    {
        if (!CheckIfCanDrop())
            return;

        PlayerBase homeBase = player.HomeBase;

        if (homeBase != null)
            homeBase.Snowballs++;

        player.Inventory.Snowballs--;

        player.RaiseSnowballChangedEvent();
    }

    private bool CheckIfCanPickUp()
    {
        if (!player.NearPickableBase)
            return false;

        if (!player.Inventory.IsInventoryFull())
            return true;
        else
            return false;
    }

    private bool CheckIfCanShoot()
    {
        if (player.Inventory.IsInventoryEmpty())
            return false;
        else if (isShooting)
            return false;
        else
            return true;
    }

    private bool CheckIfCanDrop()
    {
        if (!player.NearHome)
            return false;

        if (player.Inventory.IsInventoryEmpty())
            return false;
        else
            return true;
    }
}
