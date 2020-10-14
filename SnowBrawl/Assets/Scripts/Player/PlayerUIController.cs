using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    
    [Header("Snowballs")]
    [SerializeField] private List<GameObject> snowballIcons = new List<GameObject>(3);
    [SerializeField] private GameObject snowballSlotsParent;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI deathTimer;
    [SerializeField] private TextMeshProUGUI homeSnowballs;

    [Header("Icons")]
    [SerializeField] private Sprite aliveIcon;
    [SerializeField] private Sprite deadIcon;

    [Space(10f)]
    [SerializeField] private PlayerBase playerBase;
    [SerializeField] private Image characterImage;

    private float currentDeathTime;
    private float respawnTime;
    
    private bool startDeathTimer;

    private void Awake()
    {
        GameManager.playerSpawned += OnPlayerSpawned;
        GameManager.playerKilled += OnPlayerKilled;

        respawnTime = GameManager.RespawnTime;
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

        StartTimer();

        snowballSlotsParent.SetActive(false);

        deathTimer.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.GameIsPaused)
            return;

        if (!startDeathTimer)
            return;

        deathTimer.text = currentDeathTime.ToString("0");

        currentDeathTime -= Time.deltaTime;

        if (currentDeathTime <= 0)
            startDeathTimer = false;
    }

    private void StartTimer()
    {
        currentDeathTime = respawnTime;

        startDeathTimer = true;
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

    private GameManager GameManager { get { return GameManager.Instance; } }
}
