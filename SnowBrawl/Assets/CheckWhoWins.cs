using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWhoWins : MonoBehaviour {

    public GameObject p1WinText;
    public GameObject p2WinText;
    public GameObject drawText;

    void Start () 
    {
        int p1Wins = GameManager.p1Wins;
        int p2Wins = GameManager.p2Wins;

        if (p1Wins > p2Wins)
            p1WinText.SetActive(true);
        else if (p1Wins < p2Wins)
            p2WinText.SetActive(true);
        else
            drawText.SetActive(true);
	}

    public void PlayAgain()
    {
        SceneManager.LoadScene("InstructionsPt1");
    }
}
