using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour {

    public GameObject p1;
    public GameObject p2;
    public GameObject p1Prefab;
    public GameObject p2Prefab;
    private GameObject p1Col;
    private GameObject p2Col;
    public GameObject p1SnowballIcon1;
    public GameObject p1SnowballIcon2;
    public GameObject p1SnowballIcon3;
    public GameObject p2SnowballIcon1;
    public GameObject p2SnowballIcon2;
    public GameObject p2SnowballIcon3;
    public Vector3 p1SpawnPosition = new Vector3(-27f, 15f, 0f);
    public Vector3 p2SpawnPosition = new Vector3(18f, 15f, 0f);
    public static int p1SnowballsN = 0;
    public static int p2SnowballsN = 0;
    public Text p1SnowballsNText;
    public Text p2SnowballsNText;
    public static int p1BaseSnowballsN = 0;
    public static int p2BaseSnowballsN = 0;
    public static int p1MaximumSnowballsN = 2;
    public static int p2MaximumSnowballsN = 2;
    public Text p1BaseSnowballsNText;
    public Text p2BaseSnowballsNText;
    public static bool p1Dead = false;
    public static bool p2Dead = false;
    public GameObject p1Icon;
    public GameObject p2Icon;
    public GameObject p1DeadIcon;
    public GameObject p2DeadIcon;
    public Text p1DeathTimer;
    public Text p2DeathTimer;
    private bool showDeathTimerP1 = false;
    private bool showDeathTimerP2 = false;
    private float deathTimerP1;
    private float deathTimerP2;
    public float _deathTimer;

    public GameObject round1;
    public GameObject round2;
    public GameObject round3;

    public static bool p1HasPU = false;
    public static bool p2HasPU = false;
    public static bool p1HasGlovesPU = false;
    public static bool p2HasGlovesPU = false;
    public static bool p1HasBootsPU = false;
    public static bool p2HasBootsPU = false;
    public static bool p1HasShieldPU = false;
    public static bool p2HasShieldPU = false;
    public static bool p1HasESPU = false;
    public static bool p2HasESPU = false;
    public static bool p1PUTextShown = false;
    public static bool p2PUTextShown = false;

    private void DisplayNOfSnowballs()
    {
        //p1SnowballsNText.text = p1SnowballsN.ToString();
        //p2SnowballsNText.text = p2SnowballsN.ToString();

        p1BaseSnowballsNText.text = p1BaseSnowballsN.ToString();
        p2BaseSnowballsNText.text = p2BaseSnowballsN.ToString(); 
    }

    public void CheckIfPlayerIsDead()
    {
        //p1.GetComponent<Player1Controller>().CheckForIcons();
        if (p1Dead)
        {
            p1Col = GameObject.FindWithTag("Player1");
            p1Col.GetComponent<Player1Controller>().SetAllPUToFalse();
            p1Icon.SetActive(false);
            p1DeadIcon.SetActive(true);
            showDeathTimerP1 = true;
            p1DeathTimer.gameObject.SetActive(true);
            deathTimerP1 = _deathTimer;
            StartCoroutine(RespawnP1());
            Destroy(p1Col.gameObject);
        }
        if (p2Dead)
        {
            p2Col = GameObject.FindWithTag("Player2");
            p2Col.GetComponent<Player2Controller>().SetAllPUToFalse();
            p2Icon.SetActive(false);
            p2DeadIcon.SetActive(true);
            showDeathTimerP2 = true;
            p2DeathTimer.gameObject.SetActive(true);
            deathTimerP2 = _deathTimer;
            StartCoroutine(RespawnP2());
            Destroy(p2Col.gameObject);
        }
    }
    public IEnumerator RespawnP1()
    {
        p1Dead = false;
        yield return new WaitForSeconds(8f);
        p1HasPU = false;
        showDeathTimerP1 = false;
        p1DeathTimer.gameObject.SetActive(false);
        p1Icon.SetActive(true);
        p1DeadIcon.SetActive(false);
        if(SceneManager.GetActiveScene().name == "Round3")
        {
            p1SpawnPosition = new Vector3(-15f, 14f, 0f);
        }
        GameObject Player1 = Instantiate(p1Prefab, p1SpawnPosition, Quaternion.identity);
        Player1.name = "Player1";
        p1Col = GameObject.FindWithTag("Player1");
        if(p2Col)
        {
            Physics2D.IgnoreCollision(p1Col.GetComponent<BoxCollider2D>(), p2Col.GetComponent<BoxCollider2D>());
        }
    }
    public IEnumerator RespawnP2()
    {
        p2Dead = false;
        yield return new WaitForSeconds(8f);
        p2HasPU = false;
        showDeathTimerP2 = false;
        p2DeathTimer.gameObject.SetActive(false);
        p2Icon.SetActive(true);
        p2DeadIcon.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Round3")
        {
            p2SpawnPosition = new Vector3(7f, 14f, 0f);
        }
        GameObject Player2 = Instantiate(p2Prefab, p2SpawnPosition, Quaternion.identity);
        Player2.name = "Player2";
        p2Col = GameObject.FindWithTag("Player2");
        if (p1Col)
        {
            Physics2D.IgnoreCollision(p1Col.GetComponent<BoxCollider2D>(), p2Col.GetComponent<BoxCollider2D>());
        }
    }

    public void SnowballIconsManage()
    {
        //p1 snowballicons managing
        if(p1SnowballsN == 1)
        {
            p1SnowballIcon1.SetActive(true);
            p1SnowballIcon2.SetActive(false);
            p1SnowballIcon3.SetActive(false);
        } else if (p1SnowballsN == 2)
        {
            p1SnowballIcon2.SetActive(true);
            p1SnowballIcon3.SetActive(false);
        } else if (p1SnowballsN == 3)
        {
            p1SnowballIcon3.SetActive(true);
        } else
        {
            p1SnowballIcon1.SetActive(false); 
            p1SnowballIcon2.SetActive(false);
            p1SnowballIcon3.SetActive(false);
        }
        //p2 snowballicons managing
        if (p2SnowballsN == 1)
        {
            p2SnowballIcon1.SetActive(true);
            p2SnowballIcon2.SetActive(false);
            p2SnowballIcon3.SetActive(false);
        } else if (p2SnowballsN == 2)
        {
            p2SnowballIcon2.SetActive(true);
            p2SnowballIcon3.SetActive(false);
        } else if (p2SnowballsN == 3)
        {
            p2SnowballIcon3.SetActive(true);
        } else
        {
            p2SnowballIcon1.SetActive(false);
            p2SnowballIcon2.SetActive(false);
            p2SnowballIcon3.SetActive(false);
        }
    }

    public IEnumerator StartGameCO()
    {
        //add round image
        if(SceneManager.GetActiveScene().name == "Round1")
        {
            round1.SetActive(true);
        } else if(SceneManager.GetActiveScene().name == "Round2")
        {
            round2.SetActive(true);
        } else if(SceneManager.GetActiveScene().name == "Round3")
        {
            round3.SetActive(true);
        }
        yield return new WaitForSeconds(4f);
        //remove round image
        if (SceneManager.GetActiveScene().name == "Round1")
        {
            round1.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Round2")
        {
            round2.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Round3")
        {
            round3.SetActive(false);
        }
        //GameObject Player1 = Instantiate(p1, p1SpawnPosition, Quaternion.identity);
        //Player1.name = "Player1";
        //GameObject Player2 = Instantiate(p2, p2SpawnPosition, Quaternion.identity);
        //Player2.name = "Player2";
    }

    private void Start()
    {
        p1Col = GameObject.FindWithTag("Player1");
        //p2Col = GameObject.FindWithTag("Player2");
        //Physics2D.IgnoreCollision(p1Col.GetComponent<BoxCollider2D>(), p2Col.GetComponent<BoxCollider2D>());
        StartCoroutine(StartGameCO());
    }

    private void Update()
    {
        DisplayNOfSnowballs();
        CheckIfPlayerIsDead();
        SnowballIconsManage();
        if(showDeathTimerP1)
        {
            deathTimerP1 -= Time.deltaTime;
            p1DeathTimer.text = deathTimerP1.ToString("0");
        }
        if (showDeathTimerP2)
        {
            deathTimerP2 -= Time.deltaTime;
            p2DeathTimer.text = deathTimerP2.ToString("0");
        }
    }
}
