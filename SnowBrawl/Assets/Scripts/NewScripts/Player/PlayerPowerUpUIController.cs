using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUpUIController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;

    [SerializeField] private Image powerUpIcon;

    [SerializeField] private TextMeshProUGUI powerUpTimerText;

    private PowerUpType powerUpType;

    private float powerUpTimer;

    private bool playerHasPowerUp;

    private void Start()
    {
        GameManager.Instance.playerSpawned += OnPlayerSpawned;
        GameManager.Instance.playerKilled += OnPlayerKilled;
    }

    private void OnPlayerSpawned(Player player)
    {
        if (player.PlayerID != playerID)
            return;

        player.onPlayerReceivedPowerUp += OnPlayerReceivedPowerUp;
    }

    private void OnPlayerKilled(Player player)
    {
        if (player.PlayerID != playerID)
            return;

        ResetPowerUp();
    }

    private void OnPlayerReceivedPowerUp(PowerUpData powerUpData)
    {
        if (powerUpData.playerID != playerID)
            return;

        powerUpTimer = powerUpData.powerUpDuration;

        powerUpIcon.gameObject.SetActive(true);

        playerHasPowerUp = true;
    }

    private void Update()
    {
        if (playerHasPowerUp)
        {
            powerUpTimer -= Time.deltaTime;
            powerUpTimerText.text = powerUpTimer.ToString("0");
            
            if (powerUpTimer < 0)
                ResetPowerUp();
        }
    }

    private void ResetPowerUp()
    {
        playerHasPowerUp = false;
        powerUpIcon.gameObject.SetActive(false);
    }
}
