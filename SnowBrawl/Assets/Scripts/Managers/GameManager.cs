using System;
using System.Collections.Generic;
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

    [Header("Settings")]
    [SerializeField] private MovementSettings mvSettings;

    [Header("Script References")]
    [SerializeField] private PlayersSpawner playersSpawner;
    [SerializeField] private PlayerBase p1Base;
    [SerializeField] private PlayerBase p2Base;
    [SerializeField] private RoundTimer roundTimer;

    [Header("Timer")]
    [SerializeField] private int roundDuration;

    [Header("UI References")]
    [SerializeField] private List<GameObject> objectToEnableOnRoundOver;
    [Space(5f), SerializeField] private TextMeshProUGUI playerXWon;
    [SerializeField] private TextMeshProUGUI score;

    public MovementSettings MVSettings { get { return mvSettings; } }

    public bool GameIsPaused { get; private set; }

    public int RoundDuration { get { return roundDuration; } }
    public int RespawnTime { get { return playersSpawner.RespawnTime; } }

    private int p1Wins;
    private int p2Wins;

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
        SoundManager.PlaySound(Sound.Round_Over);

        if (onRoundOver != null)
            onRoundOver.Invoke();
     
        GameIsPaused = true;

        SaveWins();
        
        if (SBSceneManager.IsLastRound())
            ShowGameOverUI();
        else
            ShowRoundOverUI();
    }

    private void ShowRoundOverUI()
    {
        objectToEnableOnRoundOver.ForEach(obj => obj.SetActive(true));

        playerXWon.text = GetPlayerWonText();
    }

    private void ShowGameOverUI()
    {
        objectToEnableOnRoundOver.ForEach(obj => obj.SetActive(true));

        playerXWon.text = GetResultsText();

        if (score == null)
            return;

        score.text = p1Wins + " - " + p2Wins;
    }

    public void KillPlayer(Player player)
    {
        SoundManager.PlaySound(Sound.SnowballHit);
        
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
        if (SBSceneManager.Instance == null)
            return;

        SBSceneManager.LoadNextRound();
    }

    public void PlayAgain()
    {
        SBSceneManager.LoadFirstRound();
    }

    public void GoToMainMenu()
    {
        SBSceneManager.LoadMainMenu();
    }

    public void SaveWins()
    {
        int p1BaseSnowball = p1Base.Snowballs;
        int p2BaseSnowball = p2Base.Snowballs;

        if (p1BaseSnowball > p2BaseSnowball)
            p1Wins++;
        else if (p1BaseSnowball < p2BaseSnowball)
            p2Wins++;
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

    public string GetResultsText()
    {
        if (p1Wins > p2Wins)
            return "Player 1 Won";
        else if (p1Wins < p2Wins)
            return "Player 2 Won";
        else
            return "Draw";
    }

    private SBSceneManager SBSceneManager { get { return SBSceneManager.Instance; } }
}
