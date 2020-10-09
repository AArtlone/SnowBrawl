using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite powerUpIcon;
    
    [SerializeField] private PowerUpType powerUpType;

    [SerializeField] private float powerUpDuration = 20f;

    private void OnTriggerStay2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();

        if (player == null)
            return;

        if (player.PowerUpsManager.HasAnyPowerUp)
            return;

        var powerUpData = new PowerUpData(player.PlayerID, powerUpType, powerUpIcon, powerUpDuration);

        player.ReceivePowerUp(powerUpData);

        GameManager.Instance.PowerUpWasPickedUp();

        Destroy(gameObject);
    }
}

public class PowerUpData
{
    public PlayerID playerID;
    public PowerUpType powerUpType;
    public Sprite powerUpIcon;
    public float powerUpDuration;

    public PowerUpData(PlayerID playerID, PowerUpType powerUpType, Sprite powerUpIcon, float powerUpDuration)
    {
        this.playerID = playerID;
        this.powerUpType = powerUpType;
        this.powerUpIcon = powerUpIcon;
        this.powerUpDuration = powerUpDuration;
    }
}
