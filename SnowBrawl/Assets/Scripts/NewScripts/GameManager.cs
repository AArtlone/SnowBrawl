using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action onRoundStart;
    public Action onRoundOver;
    public Action onPowerUpPickUp;

    public Action<Player> playerSpawned;
    public Action<Player> playerKilled;

    [SerializeField] private MovementSettings mvSettings;

    [SerializeField] private PlayersSpawner playersSpawner;

    [SerializeField] private RoundTimer roundTimer;

    [SerializeField] private int roundDuration;

    [SerializeField] private PlayerBase p1Base;
    [SerializeField] private PlayerBase p2Base;

    [Header("UI References")]
    [SerializeField] private GameObject roundOverObject;
    [SerializeField] private TextMeshProUGUI playerXWon;
    [SerializeField] private GameObject nextRoundButton;

    public MovementSettings MVSettings { get { return mvSettings; } }

    public bool GameIsPaused { get; private set; }

    public int RoundDuration { get { return roundDuration; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        GameIsPaused = true;
    }

    private void Start()
    {
        playersSpawner.SpawnPlayer1();
        playersSpawner.SpawnPlayer2();
    }

    public void RoundStart()
    {
        if (onRoundStart != null)
            onRoundStart.Invoke();

        GameIsPaused = false;

        roundTimer.StartTimer();
    }

    public void RoundOver()
    {
        if (onRoundOver != null)
            onRoundOver.Invoke();
     
        GameIsPaused = true;

        ShowRoundOverUI();
    }

    private void ShowRoundOverUI()
    {
        roundOverObject.SetActive(true);

        playerXWon.gameObject.SetActive(true);

        playerXWon.text = GetPlayerWonText();

        nextRoundButton.SetActive(true);
    }

    public void KillPlayer(Player player)
    {
        if (player.PowerUpsManager.HasPowerUp(PowerUpType.Shield))
            return;

        player.Kill();

        if (playerKilled != null)
            playerKilled.Invoke(player);

        playersSpawner.RespawnPlayer(player.PlayerID);
    }

    public void PlayerWasSpawned(Player player)
    {
        if (playerSpawned != null)
            playerSpawned.Invoke(player);
    }

    public void PowerUpWasPickedUp()
    {
        if (onPowerUpPickUp != null)
            onPowerUpPickUp.Invoke();
    }

    public void LoadNextLevel()
    {
        SBSceneManager.Instance.LoadNextRound();
    }

    public string GetPlayerWonText()
    {
        int p1BaseSnowball = p1Base.Snowballs;
        int p2BaseSnowball = p2Base.Snowballs;

        if (p1BaseSnowball > p2BaseSnowball)
            return "Player 1 Won";
        else if (p1BaseSnowball < p2BaseSnowball)
            return "Player 2 Won";
        else
            return "Draw";
    }
}
