using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action<Player> playerSpawned;
    public Action<Player> playerKilled;

    [SerializeField] private MovementSettings mvSettings;

    [SerializeField] private PlayersSpawner playersSpawner;

    public MovementSettings MVSettings { get { return mvSettings; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playersSpawner.SpawnPlayer1();
        playersSpawner.SpawnPlayer2();
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
}
