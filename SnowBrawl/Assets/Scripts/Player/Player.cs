using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<int> numOfSnowballChanged;

    public Action<PowerUpData> onPlayerReceivedPowerUp;

    [SerializeField] private PlayerID playerID;
    [SerializeField] private PowerUpsManager powerUpsManager;
    [SerializeField] private Transform playerCanvas;

    public PlayerID PlayerID { get { return playerID; } }
    public KeysSettings KeysSettings { get; private set; }
    public PowerUpsManager PowerUpsManager { get { return powerUpsManager; } }
    public Transform PlayerCanvas { get { return playerCanvas; } }
    public Inventory Inventory { get; private set; }
    public PlayerBase NearbyPlayerBase { get; private set; }

    public bool NearPickableBase { get; private set; }
    public bool FacingRight { get; set; }

    private void Awake()
    {
        var fileName = playerID.ToString() + "KeysSettings.json";

        if (!IOHandler.FileExists(fileName))
        {
            Debug.LogError("The " + fileName + " file does not exist");
            return;
        }

        var jsonData = IOHandler.LoadFile<KeysSettings>(fileName);
        
        KeysSettings = jsonData;
    }

    private void Start()
    {
        Inventory = new Inventory(this);
    }

    public void ReceivePowerUp(PowerUpData powerUpData)
    {
        if (onPlayerReceivedPowerUp != null)
            onPlayerReceivedPowerUp(powerUpData);

        powerUpsManager.PowerUpWasPickedUp(powerUpData);
    }

    public void EnteredPickableBase()
    {
        NearPickableBase = true;
    }

    public void EnteredPickableBase(PlayerBase playerBase)
    {
        NearbyPlayerBase = playerBase;
        NearPickableBase = true;
    }

    public void ExitedPickableBase()
    {
        NearPickableBase = false;

        if (NearbyPlayerBase != null)
            NearbyPlayerBase = null;
    }

    public void Kill()
    {
        Inventory.Snowballs = 0;

        Destroy(gameObject);
    }

    public void RaiseSnowballChangedEvent()
    {
        if (numOfSnowballChanged != null)
            numOfSnowballChanged.Invoke(Inventory.Snowballs);
    }

    public bool IsNearHome()
    {
        if (NearbyPlayerBase == null)
            return false;

        return playerID == NearbyPlayerBase.PlayerID;
    }
}
