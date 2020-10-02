using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundTimer;

    private bool startRound;

    private float timer;

    private void Update()
    {
        if (!startRound)
            return;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            GameManager.Instance.RoundOver();
            
            // Disabling the component so this update method does not run since it is not needed
            enabled = false;
        }

        roundTimer.text = timer.ToString("0");

    }

    public void StartRound(int time)
    {
        startRound = true;

        timer = time;
    }
}
