﻿using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

        roundTimer.StartRound(roundDuration);
    }

    public void RoundStart()
    {
        GameIsPaused = false;
    }

    public void RoundOver()
    {
        GameIsPaused = true;

        //TODO: turn physics off to freeze players

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
