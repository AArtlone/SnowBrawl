using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundTimer;

    private bool startTimer;

    private float timer;

    private void Awake()
    {
        timer = GameManager.RoundDuration;

        roundTimer.text = timer.ToString();
    }

    private void Update()
    {
        if (!startTimer)
            return;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            GameManager.RoundOver();
            
            // Disabling the component so this update method does not run since it is not needed
            enabled = false;
        }

        roundTimer.text = timer.ToString("0");
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
