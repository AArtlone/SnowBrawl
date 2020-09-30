using System;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance;

    public Action<Player> playerSpawned;

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
        player.Kill();

        playersSpawner.RespawnPlayer(player.PlayerID);
    }

    public void PlayerWasSpawned(Player player)
    {
        if (playerSpawned != null)
            playerSpawned.Invoke(player);
    }
}
