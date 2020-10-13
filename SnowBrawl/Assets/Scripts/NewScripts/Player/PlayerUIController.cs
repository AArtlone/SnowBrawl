using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    [SerializeField] private List<GameObject> snowballIcons = new List<GameObject>(3);
    [SerializeField] private GameObject snowballSlotsParent;

    [SerializeField] private TextMeshProUGUI deathTimer;
    [SerializeField] private TextMeshProUGUI homeSnowballs;

    [SerializeField] private PlayerBase playerBase;

    [SerializeField] private Sprite aliveIcon;
    [SerializeField] private Sprite deadIcon;
    [SerializeField] private Image characterImage;

    private void Awake()
    {
        GameManager.Instance.playerSpawned += OnPlayerSpawned;
        GameManager.Instance.playerKilled += OnPlayerKilled;
    }

    private void Start()
    {
        if (playerBase != null)
            playerBase.snowballsChanged += OnHomeSnowballsChanged;
    }

    private void OnPlayerSpawned(Player player)
    {
        if (player.PlayerID != playerID)
            return;

        player.numOfSnowballChanged += OnNumOfSnowballesChanged;

        characterImage.sprite = aliveIcon;

        snowballSlotsParent.SetActive(true);

        deathTimer.gameObject.SetActive(false);
    }

    private void OnPlayerKilled(Player player)
    {
        if (player.PlayerID != playerID)
            return;

        characterImage.sprite = deadIcon;

        snowballSlotsParent.SetActive(false);

        deathTimer.gameObject.SetActive(true);
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
