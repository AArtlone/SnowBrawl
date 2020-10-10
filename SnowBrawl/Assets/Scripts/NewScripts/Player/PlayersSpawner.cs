using System.Collections;
using UnityEngine;

public class PlayersSpawner : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] private Player p1Prefab;
    [SerializeField] private Player p2Prefab;

    [Header("Respawn")]
    [SerializeField] private float playerRespawnTime;
    [SerializeField] private Transform p1RespawnPos;
    [SerializeField] private Transform p2RespawnPos;

    public void RespawnPlayer(PlayerID playerID)
    {
        StartCoroutine(RespawnPlayerCo(playerID));
    }

    private IEnumerator RespawnPlayerCo(PlayerID playerID)
    {
        yield return new WaitForSeconds(playerRespawnTime);

        if (GameManager.GameIsPaused)
            yield break;

        if (playerID == PlayerID.P1)
            SpawnPlayer1();
        else
            SpawnPlayer2();
    }

    public void SpawnPlayer1()
    {
        Player player = Instantiate(p1Prefab, p1RespawnPos.position, Quaternion.identity);

        GameManager.PlayerWasSpawned(player);
    }

    public void SpawnPlayer2()
    {
        Player player = Instantiate(p2Prefab, p2RespawnPos.position, Quaternion.identity);

        GameManager.PlayerWasSpawned(player);
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
