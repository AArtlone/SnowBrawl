using TMPro;
using UnityEngine;

public class StartRoundCountdown: MonoBehaviour
{
    [SerializeField] private int startGameDelay;

    [SerializeField] private TextMeshProUGUI countdownText;

    private float countdown;

    private void Awake()
    {
        countdown = startGameDelay;

        countdownText.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating("PlaySound", 0, 1);
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            CancelInvoke();

            SoundManager.PlaySound(Sound.DropSnowball);

            countdownText.gameObject.SetActive(false);
            GameManager.Instance.RoundStart();

            enabled = false; // Disabling the component for performance purposes
        }

        int numberToShow = Mathf.FloorToInt(countdown) + 1;

        countdownText.text = numberToShow.ToString();
    }

    private void PlaySound()
    {
        SoundManager.PlaySound(Sound.PowerUp);
    }
}
