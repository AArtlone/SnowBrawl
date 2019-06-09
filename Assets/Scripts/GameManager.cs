using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text gameTimerUI;
    public float gameTimer;
    public GameObject levelTimer;

    public GameObject p1;
    public GameObject p2;

    public static int p1Wins;
    public static int p2Wins;

    private PlayersManager playersManager;

    public void SetTimer()
    {
        gameTimer -= Time.deltaTime;
        gameTimerUI.text = gameTimer.ToString("0");
    }

    public void GameOver()
    {
        if(gameTimer <= 0)
        {
            if(PlayersManager.p1BaseSnowballsN > PlayersManager.p2BaseSnowballsN)
            {
                p1Wins++;
                //load next scene
                if (SceneManager.GetActiveScene().name == "Round1")
                {
                    SceneManager.LoadScene("Round2");
                    PlayersManager.p1BaseSnowballsN = 0;
                    PlayersManager.p2BaseSnowballsN = 0;
                    PlayersManager.p1SnowballsN = 0;
                    PlayersManager.p2SnowballsN = 0;
                }
                else if (SceneManager.GetActiveScene().name == "Round2")
                {
                    if(p1Wins == 2 || p2Wins == 2)
                    {
                        SceneManager.LoadScene("GameOver");
                    } else
                    {
                        SceneManager.LoadScene("Round3");
                        PlayersManager.p1BaseSnowballsN = 0;
                        PlayersManager.p2BaseSnowballsN = 0;
                        PlayersManager.p1SnowballsN = 0;
                        PlayersManager.p2SnowballsN = 0;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Round3")
                {
                    SceneManager.LoadScene("GameOver");
                }
            } else if(PlayersManager.p1BaseSnowballsN < PlayersManager.p2BaseSnowballsN)
            {
                p2Wins++;
                //load next scene
                if (SceneManager.GetActiveScene().name == "Round1")
                {
                    SceneManager.LoadScene("Round2");
                    PlayersManager.p1BaseSnowballsN = 0;
                    PlayersManager.p2BaseSnowballsN = 0;
                    PlayersManager.p1SnowballsN = 0;
                    PlayersManager.p2SnowballsN = 0;
                }
                else if (SceneManager.GetActiveScene().name == "Round2")
                {
                    if (p1Wins == 2 || p2Wins == 2)
                    {
                        SceneManager.LoadScene("GameOver");
                    }
                    else
                    {
                        SceneManager.LoadScene("Round3");
                        PlayersManager.p1BaseSnowballsN = 0;
                        PlayersManager.p2BaseSnowballsN = 0;
                        PlayersManager.p1SnowballsN = 0;
                        PlayersManager.p2SnowballsN = 0;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Round3")
                {
                    SceneManager.LoadScene("GameOver");
                }
            } else if(PlayersManager.p1BaseSnowballsN == PlayersManager.p2BaseSnowballsN)
            {
                //load next scene
                if (SceneManager.GetActiveScene().name == "Round1")
                {
                    SceneManager.LoadScene("Round2");
                    PlayersManager.p1BaseSnowballsN = 0;
                    PlayersManager.p2BaseSnowballsN = 0;
                    PlayersManager.p1SnowballsN = 0;
                    PlayersManager.p2SnowballsN = 0;
                }
                else if (SceneManager.GetActiveScene().name == "Round2")
                {
                    if (p1Wins == 2 || p2Wins == 2)
                    {
                        SceneManager.LoadScene("GameOver");
                    }
                    else
                    {
                        SceneManager.LoadScene("Round3");
                        PlayersManager.p1BaseSnowballsN = 0;
                        PlayersManager.p2BaseSnowballsN = 0;
                        PlayersManager.p1SnowballsN = 0;
                        PlayersManager.p2SnowballsN = 0;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Round3")
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }
    public IEnumerator StartGameCO()
    {
        yield return new WaitForSeconds(4f);
        levelTimer.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(StartGameCO());
        playersManager = FindObjectOfType<PlayersManager>();

    }
    private void Update()
    {
        SetTimer();
        GameOver();
    }

}
