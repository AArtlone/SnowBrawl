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
        if (GameManager.Instance.GameIsPaused)
            return;

        if (Input.GetKeyDown(throwKey))
            Shoot();

        if (Input.GetKeyDown(dropKey))
            Drop();

        CheckForPickUpAction();
    }

    private void CheckForPickUpAction()
    {
        if (player.NearPickableBase)
            CheckForPickUp();
        else if (player.NearEnemyBase)
            CheckForSteal();
    }

    private void CheckForPickUp()
    {
        if (!CheckIfCanPickUp())
            return;

        if (!WHATONAMETHIS())
            return;

        PickUp();

        if (!CheckIfCanPickUp())
            animationController.StopPickUpAnimation();
    }

    private void CheckForSteal()
    {
        if (!CheckIfCanSteal())
            return;

        if (!WHATONAMETHIS())
            return;

        PickUp();

        if (!CheckIfCanSteal())
            animationController.StopPickUpAnimation();
    }

    //TODO: rename
    private bool WHATONAMETHIS()
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
