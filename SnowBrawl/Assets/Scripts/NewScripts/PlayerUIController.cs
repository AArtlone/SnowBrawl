using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    [SerializeField] private List<GameObject> snowballIcons = new List<GameObject>(3);

    private void Start()
    {
        NewGameManager.Instance.playerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(Player player)
    {
        if (player.PlayerID == playerID)
            player.numOfSnowballChanged += OnNumOfSnowballesChanged;
    }

    private void OnNumOfSnowballesChanged(int value)
    {
        snowballIcons.ForEach(e => e.SetActive(false));

        for (int i = 0; i < value; i++)
            snowballIcons[i].SetActive(true);
    }
}
