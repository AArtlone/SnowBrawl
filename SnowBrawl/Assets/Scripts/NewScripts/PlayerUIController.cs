using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    [SerializeField] private List<GameObject> snowballIcons = new List<GameObject>(3);
    [SerializeField] private TextMeshProUGUI homeSnowballs;

    [SerializeField] private PlayerBase playerBase;

    private void Awake()
    {
        NewGameManager.Instance.playerSpawned += OnPlayerSpawned;
    }

    private void Start()
    {
        if (playerBase != null)
            playerBase.snowballsChanged += OnHomeSnowballsChanged;
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

    private void OnHomeSnowballsChanged(int value)
    {
        homeSnowballs.text = value.ToString();
    }
}
