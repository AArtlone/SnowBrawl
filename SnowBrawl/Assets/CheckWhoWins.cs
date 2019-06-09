using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWhoWins : MonoBehaviour {

    public GameObject p1WinText;
    public GameObject p2WinText;
    public GameObject drawText;

    // Use this for initialization
    void Start () {
		if(GameManager.p1Wins > GameManager.p2Wins)
        {
            p1WinText.SetActive(true);
        } else if(GameManager.p1Wins < GameManager.p2Wins)
        {
            p2WinText.SetActive(true);
        } else if(GameManager.p1Wins == GameManager.p2Wins)
        {
            drawText.SetActive(true);
        }
	}

    public void PlayAgain()
    {
        SceneManager.LoadScene("InstructionsPt1");
    }
}
