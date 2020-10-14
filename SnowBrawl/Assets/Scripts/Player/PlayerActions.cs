using System;
using UnityEngine;

public class PlayerActions: MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private SnowballThrower snowballThrower;

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
        if (GameManager.Instance.GameIsPaused)
            return;

        if (Input.GetKeyDown(throwKey))
            Throw();

        if (Input.GetKeyDown(dropKey))
            Drop();

        PickUpAction();
    }

    private void PickUpAction()
    {
        if (player.NearPickableBase)
            TryToPickUp();
    }

    private void TryToPickUp()
    {
        if (!CheckIfCanPickUp())
            return;

        if (!CheckIfPickUpWasSuccessful())
            return;

        PickUp();

        if (!CheckIfCanPickUp())
            animationController.StopPickUpAnimation();
    }

    // Checks if pick up action was succesfull or not based on the player input
    private bool CheckIfPickUpWasSuccessful()
    {
        bool pickUpKeyIsDown = Input.GetKey(pickUpKey);

        if (pickUpKeyIsDown && player.PowerUpsManager.HasPowerUp(PowerUpType.Gloves))
            return true;

        var horizontalInput = SBInputManager.Instance.GetPlayerInput(player.PlayerID);

        if (horizontalInput != 0 || !pickUpKeyIsDown)
        {
            animationController.StopPickUpAnimation();
            pickUpKeyDownTime = 0;
            return false;
        }

        animationController.StartPickUpAnimation();

        pickUpKeyDownTime += Time.deltaTime;

        if (pickUpKeyDownTime < timeToPickUp)
            return false;

        return true;
    }

    public void PickUp()
    {
        pickUpKeyDownTime = 0;
        
        player.Inventory.Snowballs++;

        if (player.NearbyPlayerBase != null)
            player.NearbyPlayerBase.Snowballs--;

        PopUpManager.PickedUpSnowball(player);

        SoundManager.PlaySound(Sound.PickUpSnowball);
    }

    public void Throw()
    {
        if (!CheckIfCanThrow())
            return;

        isShooting = true;

        var doneShootingCallback = new Action(() =>
        {
            isShooting = false;
        });

        snowballThrower.Throw(player.FacingRight, player.PlayerID, doneShootingCallback);

        animationController.ThrowAnimation();

        player.Inventory.Snowballs--;

        SoundManager.PlaySound(Sound.ThrowSnowball);
    }

    public void Drop()
    {
        if (!CheckIfCanDrop())
            return;

        PlayerBase homeBase = player.NearbyPlayerBase;

        if (homeBase != null)
            homeBase.Snowballs++;

        player.Inventory.Snowballs--;

        SoundManager.PlaySound(Sound.DropSnowball);

        PopUpManager.DroppedSnowballPopUp(homeBase);
    }

    #region HelpFunctions
    private bool CheckIfCanPickUp()
    {
        if (player.Inventory.IsInventoryFull())
            return false;

        if (!player.NearPickableBase)
            return false;

        if (player.NearbyPlayerBase != null)
        {
            return player.NearbyPlayerBase.Snowballs != 0;
        }

        return true;
    }

    private bool CheckIfCanThrow()
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
        if (!player.IsNearHome())
            return false;

        if (player.Inventory.IsInventoryEmpty())
            return false;
        else
            return true;
    } 
    #endregion

    private PopUpManager PopUpManager { get { return PopUpManager.Instance; } }
}
