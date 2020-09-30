using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance;

    [SerializeField] private MovementSettings mvSettings;

    [Header("Player Prefabs")]
    [SerializeField] private Player p1Prefab;
    [SerializeField] private Player p2Prefab;

    [Header("Respawn")]
    [SerializeField] private float playerRespawnTime;
    [SerializeField] private Transform p1RespawnPos;
    [SerializeField] private Transform p2RespawnPos;

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

    public void KillPlayer(Player player)
    {
        Destroy(player.gameObject);

        StartCoroutine(RespawnPlayerCo(player.PlayerID));
    }

    private IEnumerator RespawnPlayerCo(PlayerID playerID)
    {
        yield return new WaitForSeconds(playerRespawnTime);

        // Check if the timer did not run out while it was waiting

        if (playerID == PlayerID.P1)
            RespawnP1();
        else
            RespawnP2();
    }

    private void RespawnP1()
    {
        Instantiate(p1Prefab, p1RespawnPos.position, Quaternion.identity);
    }

    private void RespawnP2()
    {
        Instantiate(p2Prefab, p2RespawnPos.position, Quaternion.identity);
    }
}
