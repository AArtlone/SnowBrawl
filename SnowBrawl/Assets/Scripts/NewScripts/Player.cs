using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<int> numOfSnowballChanged;

    public Action<PowerUpData> onPlayerReceivedPowerUp;

    [SerializeField] private PlayerID playerID;
    [SerializeField] private KeysSettings keysSettings;
    [SerializeField] private PowerUpsManager powerUpsManager;

    public PlayerID PlayerID { get { return playerID; } }
    public KeysSettings KeysSettings { get { return keysSettings; } }
    public PowerUpsManager PowerUpsManager { get { return powerUpsManager; } }
    public Inventory Inventory { get; private set; }
    public PlayerBase HomeBase { get; private set; }
    public PlayerBase EnemyBase { get; private set; }

    public bool NearPickableBase { get; private set; }
    public bool NearHome { get; private set; }
    public bool NearEnemyBase { get; private set; }
    public bool FacingRight { get; set; }

    private void Start()
    {
        Inventory = new Inventory();
    }

    public void ReceivePowerUp(PowerUpData powerUpData)
    {
        if (onPlayerReceivedPowerUp != null)
            onPlayerReceivedPowerUp(powerUpData);

        powerUpsManager.PowerUpWasPickedUp(powerUpData);
    }

    public void EnteredHome(PlayerBase playerBase)
    {
        NearHome = true;
        NearPickableBase = true;

        HomeBase = playerBase;
    }

    public void ExitedHome()
    {
        NearHome = false;
        NearPickableBase = false;

        HomeBase = null;
    }

    public void EnteredEnemyBase(PlayerBase enemyBase)
    {
        NearEnemyBase = true;

        EnemyBase = enemyBase;
    }

    public void ExitedEnemyBase()
    {
        NearEnemyBase = false;

        EnemyBase = null;
    }

    public void EnteredPickableBase()
    {
        NearPickableBase = true;
    }

    public void ExitedPickableBase()
    {
        NearPickableBase = false;
    }

    public void Kill()
    {
        Inventory.Snowballs = 0;

        RaiseSnowballChangedEvent();

        Destroy(gameObject);
    }

    public void RaiseSnowballChangedEvent()
    {
        if (numOfSnowballChanged != null)
            numOfSnowballChanged.Invoke(Inventory.Snowballs);
    }
}
