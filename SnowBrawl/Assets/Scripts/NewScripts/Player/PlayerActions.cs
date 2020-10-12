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
        else if (player.NearEnemyBase)
            TryToSteal();
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

    private void TryToSteal()
    {
        if (!CheckIfCanSteal())
            return;

        if (!CheckIfPickUpWasSuccessful())
            return;

        PickUp();

        if (!CheckIfCanSteal())
            animationController.StopPickUpAnimation();
    }

    // Checks if pick up action was succesfull or not based on the player input
    private bool CheckIfPickUpWasSuccessful()
    {
        bool pickUpKeyIsDown = Input.GetKey(pickUpKey);

        if (pickUpKeyIsDown && player.PowerUpsManager.HasPowerUp(PowerUpType.Gloves))
            return true;

        if (!pickUpKeyIsDown)
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

        if (player.NearEnemyBase)
            player.EnemyBase.Snowballs--;

        PopUpManager.Instance.PickedUpSnowball(player);

        player.RaiseSnowballChangedEvent();
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

        //TODO: playsound

        PopUpManager.Instance.DroppedSnowballPopUp(homeBase);

        player.RaiseSnowballChangedEvent();
    }

    #region HelpFunctions
    private bool CheckIfCanPickUp()
    {
        if (player.Inventory.IsInventoryFull())
            return false;

        if (!player.NearPickableBase)
            return false;

        return true;
    }

    private bool CheckIfCanSteal()
    {
        if (player.Inventory.IsInventoryFull())
            return false;

        if (!player.NearEnemyBase)
            return false;

        if (player.EnemyBase.Snowballs == 0)
            return false;

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
        if (!player.NearHome)
            return false;

        if (player.Inventory.IsInventoryEmpty())
            return false;
        else
            return true;
    } 
    #endregion
}
