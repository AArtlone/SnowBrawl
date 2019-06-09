using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour {

    public float speed;
    public float springAbility;
    private float maxSpeed = 0.5f;
    public float powerUpTime = 5f;
    public float shootingTime = 0.1f;

    public static bool p2Grounded = false;
    public static bool p2shooting = false;
    public bool m_FacingRight = true;
    public static bool pickingSB = false;

    private Rigidbody2D rb2;
    private Player1Controller p1;
    private GameObject p2Col;
    public GameObject p1SnowballPrefab;
    private Animator anim;
    public Transform shootingPoint;
    public GameObject p2Snowball;

    public GameObject p2SnowballSlotIcon3;
    public AudioSource _audio;
    public AudioClip jump, hitfloor, shootSB, KO, collectSB, soundPU;

    private GameObject p2Slot;
    private GameObject p2Gloves;
    private GameObject p2Boots;
    private GameObject p2ES;
    private GameObject p2Shield;

    //checking if the button for snowball creation is held down and for how long
    private bool isDown;
    private float downTime;
    //time needed to make a snowball
    public float timeNeeded;
    
    public GameObject glovesPUIcon;
    public GameObject bootsPUIcon;
    public GameObject shieldPUIcon;
    public GameObject extraSlotPUIcon;
    public GameObject textPuGO;
    public Text textPU;
    private bool showPUTimer = false;
    private float timerPU;

    public void SetAllPUToFalse()
    {
        if (PlayersManager.p2HasGlovesPU)
        {
            p2Gloves.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p2HasBootsPU)
        {
            p2Boots.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p2HasShieldPU)
        {
            p2Shield.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p2HasESPU)
        {
            p2ES.GetComponent<Image>().enabled = false;
            p2Slot.GetComponent<Image>().enabled = false;
        }
        if (PlayersManager.p2PUTextShown)
        {
            textPuGO.GetComponent<Text>().enabled = false;
        }
    }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        p1 = FindObjectOfType<Player1Controller>();
        anim = gameObject.GetComponent<Animator>();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), p2Snowball.GetComponent<Collider2D>());
        _audio = GetComponent<AudioSource>();
        p2Boots = GameObject.FindWithTag("P2Gloves");
        p2ES = GameObject.FindWithTag("P2ES");
        p2Gloves = GameObject.FindWithTag("P2Gloves");
        p2Slot = GameObject.FindWithTag("P2SBSlot");
        p2Slot = GameObject.FindWithTag("P2Shield");
        textPuGO = GameObject.FindWithTag("P2Text");
    }

    public void PlayHitSound()
    {
        _audio.PlayOneShot(hitfloor);
    }

    public void Player2Controls()
    {
        //fliping character
        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_FacingRight)
            {
                Flip();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!m_FacingRight)
            {
                Flip();
            }
        }*/
        //jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && p2Grounded == true)
        {
            rb2.velocity = Vector2.up * springAbility;
            _audio.PlayOneShot(jump);
        }
        //shooting
        if (Input.GetKeyUp(KeyCode.P))
        {
            Shoot();
        }

        //snowball making
        if (SnowBallMaker.p2NearSnowballBase)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (PlayersManager.p2SnowballsN < PlayersManager.p2MaximumSnowballsN)
                {
                    isDown = true;
                    pickingSB = true;
                }
                else
                {
                    Debug.Log("you cant carry no more");
                    pickingSB = false;
                    //let player know that he cant carry more
                }
            }
            else if (Input.GetKeyUp(KeyCode.O))
            {
                isDown = false;
                pickingSB = false;
            }
            if (isDown)
            {
                if (PlayersManager.p2SnowballsN < PlayersManager.p2MaximumSnowballsN)
                {
                    downTime += Time.deltaTime;
                    if (downTime >= timeNeeded)
                    {
                        _audio.PlayOneShot(collectSB);
                        PlayersManager.p2SnowballsN++;
                        Debug.Log("snowball made");
                        downTime = 0f;
                    }
                }
                else
                {
                    pickingSB = false;
                    Debug.Log("you cant carry no more"); 
                }
            }
            else if (!isDown)
            {
                downTime = 0f;
            }
        }

        //snowball stealing
        if (P1Base.p2NearP1Base)
        {
            if (PlayersManager.p1BaseSnowballsN > 0)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (PlayersManager.p2SnowballsN < PlayersManager.p2MaximumSnowballsN)
                    {
                        isDown = true;
                        pickingSB = true;
                    }
                    else
                    {
                        pickingSB = false;
                        //let player know that he cant carry more
                    }
                }
                else if (Input.GetKeyUp(KeyCode.O))
                {
                    isDown = false;
                    pickingSB = false;
                }
                if (isDown)
                {
                    if (PlayersManager.p2SnowballsN < PlayersManager.p2MaximumSnowballsN)
                    {
                        _audio.PlayOneShot(collectSB);
                        downTime += Time.deltaTime;
                        if (downTime >= timeNeeded)
                        {
                            PlayersManager.p2SnowballsN++;
                            PlayersManager.p1BaseSnowballsN--;
                            Debug.Log("snowball made"); 
                            downTime = 0f;
                        }
                    }
                    else
                    {
                        pickingSB = false;
                        Debug.Log("you cant carry no more");
                    }
                }
                else if (!isDown)
                {
                    downTime = 0f;
                }
            }
            else
            {
                pickingSB = false;
                //print msg that no snowballs can be stolen
            }
        }

        //snowball dropping
        if (P2Base.p2NearBase)
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                if (PlayersManager.p2SnowballsN > 0)
                {
                    _audio.PlayOneShot(collectSB);
                    PlayersManager.p2BaseSnowballsN++;
                    PlayersManager.p2SnowballsN--;
                }
            }
        }
    }
    public void SpeedControl()
    {
        float moveHorizontal = Input.GetAxis("P2Horizontal");
        rb2.AddForce(Vector2.right * speed * moveHorizontal);

        if (rb2.velocity.x > maxSpeed)
        {
            rb2.velocity = new Vector2(maxSpeed, rb2.velocity.y);
        }
        if (rb2.velocity.x < -maxSpeed)
        {
            rb2.velocity = new Vector2(-maxSpeed, rb2.velocity.y);
        }
        if(moveHorizontal == 0)
        {
            rb2.velocity = new Vector2(0, rb2.velocity.y); 
        }
    }

    /*public void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }*/

    public void Shoot()
    {
        if (PlayersManager.p2SnowballsN > 0)
        {
            p2shooting = true;
            _audio.PlayOneShot(shootSB);
            //Debug.Log("BITCH, IT'S " + p1shooting);
            ShootingAnimation();
            PlayersManager.p2SnowballsN--;
            //possibly print a message that dont have enough snowballs    
        }
    }

    public void ShootingAnimation()
    {
        StartCoroutine(ShootingCo());
    }

    public IEnumerator ShootingCo()
    {
        timeNeeded /= 2;
        yield return new WaitForSeconds(shootingTime);
        timeNeeded *= 2;
        p2shooting = false;
        GameObject p2Bulleyt = Instantiate(p2Snowball, shootingPoint.position, shootingPoint.rotation);
        if (PlayersManager.p1HasShieldPU)
        {
            Physics2D.IgnoreCollision(p2Bulleyt.GetComponent<Collider2D>(), GameObject.FindWithTag("Player1").GetComponent<Collider2D>());
        }
    }

    //powerups
    public void GLovesPU()
    {
        StartCoroutine(GlovesCo());
    }
    public IEnumerator GlovesCo()
    {
        p2Gloves = GameObject.FindWithTag("P2Gloves");
        timeNeeded /= 2;
        p2Gloves.GetComponent<Image>().enabled = true; //p2Gloves.SetActive(true);
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p2HasPU = true;
        PlayersManager.p2HasGlovesPU = true;
        PlayersManager.p2PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime); 
        timeNeeded *= 2;
        p2Gloves.GetComponent<Image>().enabled = false; //p2Gloves.SetActive(false);
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p2HasPU = false;
        PlayersManager.p2HasGlovesPU = false;
        PlayersManager.p2PUTextShown = false;
    }
    public void BootsPU()
    {
        StartCoroutine(BootsCo());
    }
    public IEnumerator BootsCo()
    {
        p2Boots = GameObject.FindWithTag("P2Boots");
        speed *= 2;
        p2Boots.GetComponent<Image>().enabled = true; //p2Boots.SetActive(true);
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p2HasPU = true;
        PlayersManager.p2HasBootsPU = true;
        PlayersManager.p2PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        speed /= 2;
        p2Boots.GetComponent<Image>().enabled = false;
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p2HasPU = false;
        PlayersManager.p2HasBootsPU = false;
        PlayersManager.p2PUTextShown = false;
    }
    public void ExtraSlotPU()
    {
        StartCoroutine(ExtraSlotCo());
    }
    public IEnumerator ExtraSlotCo()
    {
        p2ES = GameObject.FindWithTag("P2ES");
        p2Slot = GameObject.FindWithTag("P2SBSlot");
        PlayersManager.p2MaximumSnowballsN++;
        p2ES.GetComponent<Image>().enabled = true; //p2Slot.SetActive(true);
        p2Slot.GetComponent<Image>().enabled = true;
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p2HasPU = true;
        PlayersManager.p2HasESPU = true;
        PlayersManager.p2PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        PlayersManager.p2MaximumSnowballsN--;
        p2ES.GetComponent<Image>().enabled = false;
        p2Slot.GetComponent<Image>().enabled = false;
        if (PlayersManager.p2SnowballsN > 2)
        {
            PlayersManager.p2SnowballsN = 2;
        }
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p2HasPU = false;
        PlayersManager.p2HasESPU = false;
        PlayersManager.p2PUTextShown = false;
    }
    public void ShieldPU()
    {
        StartCoroutine(ShieldCO());
    }
    public IEnumerator ShieldCO()
    {
        p2Shield = GameObject.FindWithTag("P2Shield");
        p2Shield.GetComponent<Image>().enabled = true;
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p2HasPU = true;
        PlayersManager.p2HasShieldPU = true;
        PlayersManager.p2PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        p2Shield.GetComponent<Image>().enabled = false;
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p2HasPU = false;
        PlayersManager.p2HasShieldPU = false;
        PlayersManager.p2PUTextShown = false;
    }


    void Update()
    {
        anim.SetBool("grounded", p2Grounded);
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("P2Horizontal")));

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            transform.localScale = new Vector3(-1, 1, 1);
            m_FacingRight = false;

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.localScale = new Vector3(1, 1, 1);
            m_FacingRight = true;

        }

        Player2Controls();
        anim.SetBool("shooting", p2shooting);
        anim.SetBool("picking sb", pickingSB);

        if (showPUTimer)
        {
            textPuGO = GameObject.FindWithTag("P2Text");
            timerPU -= Time.deltaTime;
            textPuGO.GetComponent<Text>().text = timerPU.ToString("0");
        }
    }

    void FixedUpdate()
    {
        SpeedControl();
    }
}
