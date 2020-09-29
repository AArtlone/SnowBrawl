using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour {

    public float speed;
    public float springAbility;
    private float maxSpeed = 0.5f;
    public float powerUpTime = 5f;
    public float shootingTime = 0.1f;

    public static bool p1Grounded = false;
    public static bool p1shooting = false;
    public bool m_FacingRight = true;
    public static bool pickingSB = false;

    private Rigidbody2D rb1;
    private Player2Controller p2;
    private Animator anim;
    public Transform shootingPoint;
    public GameObject p1Snowball;

    public GameObject p1SnowballSlotIcon3;
    public AudioSource _audio;
    public AudioClip jump, hitfloor, shootSB, KO, collectSB, soundPU;

    //checking if the button for snowball creation is held down and for how long
    private bool isDown;
    private float downTime;
    //time needed to make a snowball
    public float timeNeeded;

    public GameObject glovesPUIcon;
    public GameObject bootsPUIcon;
    public GameObject shieldPUIcon;
    public GameObject extraSlotPUIcon;
    public Text textPU;
    private bool showPUTimer = false;
    private float timerPU;

    private GameObject p1Slot;
    private GameObject p1Gloves;
    private GameObject p1Boots;
    private GameObject p1ES;
    private GameObject p1Shield;
    public GameObject textPuGO;



    public void SetAllPUToFalse()
    {
        if (PlayersManager.p1HasGlovesPU)
        {
            p1Gloves.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p1HasBootsPU)
        {
            p1Boots.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p1HasShieldPU)
        {
            p1Shield.GetComponent<Image>().enabled = false;
        }
        else if (PlayersManager.p1HasESPU)
        {
            p1ES.GetComponent<Image>().enabled = false;
            p1Slot.GetComponent<Image>().enabled = false;
        }
        if (PlayersManager.p1PUTextShown)
        {
            textPuGO.GetComponent<Text>().enabled = false;
        }
    }


    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        p2 = FindObjectOfType<Player2Controller>();
        anim = gameObject.GetComponent<Animator>();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), p1Snowball.GetComponent<Collider2D>());
        _audio = GetComponent<AudioSource>();
        p1Boots = GameObject.FindWithTag("P1Gloves");
        p1ES = GameObject.FindWithTag("P1ES");
        p1Gloves = GameObject.FindWithTag("P1Gloves");
        p1Slot = GameObject.FindWithTag("P1SBSlot");
        p1Slot = GameObject.FindWithTag("P1Shield");
        textPuGO = GameObject.FindWithTag("P1Text");
    }

    public void PlayHitSound()
    {
        _audio.PlayOneShot(hitfloor);

    }

    public void PLayer1Controls()
    {
        //fliping character
        /*if (Input.GetKeyDown("a"))
        {
            if (m_FacingRight)
            {
                Flip();
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (!m_FacingRight)
            {
                Flip();
            }
        }*/
        //jumping
        if (Input.GetKeyDown("w") && p1Grounded == true)
        {
            rb1.velocity = Vector2.up * springAbility;
            _audio.PlayOneShot(jump);
        }
        //shooting
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }

        //snowball making
        if (SnowBallMaker.p1NearSnowballBase)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if(PlayersManager.p1SnowballsN < PlayersManager.p1MaximumSnowballsN)
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
            else if (Input.GetKeyUp(KeyCode.V))
            {
                isDown = false;
                pickingSB = false;
            }
            if(isDown)
            {
                if (PlayersManager.p1SnowballsN < PlayersManager.p1MaximumSnowballsN)
                {
                    downTime += Time.deltaTime;
                    if (downTime >= timeNeeded)
                    {
                        _audio.PlayOneShot(collectSB);
                        PlayersManager.p1SnowballsN++;
                        Debug.Log("snowball made");
                        downTime = 0f;
                    }
                } else
                {
                    pickingSB = false;
                    Debug.Log("you cant carry no more");
                }
            } else if(!isDown)
            {
                downTime = 0f;
            }
        }

        //snowball stealing
        if(P2Base.p1NearP2Base)
        {
            if(PlayersManager.p2BaseSnowballsN > 0)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (PlayersManager.p1SnowballsN < PlayersManager.p1MaximumSnowballsN)
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
                else if (Input.GetKeyUp(KeyCode.V))
                {
                    isDown = false;
                    pickingSB = false;
                }
                if (isDown)
                {
                    if (PlayersManager.p1SnowballsN < PlayersManager.p1MaximumSnowballsN)
                    {
                        _audio.PlayOneShot(collectSB);
                        downTime += Time.deltaTime;
                        if (downTime >= timeNeeded)
                        {
                            PlayersManager.p1SnowballsN++;
                            PlayersManager.p2BaseSnowballsN--;
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
            } else
            {
                pickingSB = false;
                //print msg that no snowballs can be stolen
            }
        }

        //snowball dropping
        if(P1Base.p1NearBase)
        {
            if(Input.GetKeyUp(KeyCode.B))
            {
                if(PlayersManager.p1SnowballsN > 0)
                {
                    _audio.PlayOneShot(collectSB);
                    PlayersManager.p1BaseSnowballsN++;
                    PlayersManager.p1SnowballsN--;
                }
            }
        }
    }
    public void SpeedControl()
    {
        float moveHorizontal = Input.GetAxis("P1Horizontal");
        rb1.AddForce(Vector2.right * speed * moveHorizontal);

        if (rb1.velocity.x > maxSpeed)
        {
            rb1.velocity = new Vector2(maxSpeed, rb1.velocity.y);
        }
        if (rb1.velocity.x < -maxSpeed)
        {
            rb1.velocity = new Vector2(-maxSpeed, rb1.velocity.y);
        }
        if (moveHorizontal == 0)
        {
            rb1.velocity = new Vector2(0, rb1.velocity.y);
        }
    }
    /*public void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }*/

    public void Shoot()
    {
        if (PlayersManager.p1SnowballsN > 0)
        {
            p1shooting = true;
            _audio.PlayOneShot(shootSB);
            //Debug.Log("BITCH, IT'S " + p1shooting);
            ShootingAnimation();
            PlayersManager.p1SnowballsN--;
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
        p1shooting = false;
        GameObject p1Bulleyt = Instantiate(p1Snowball, shootingPoint.position, shootingPoint.rotation);
        if(PlayersManager.p2HasShieldPU)
        {
            Physics2D.IgnoreCollision(p1Bulleyt.GetComponent<Collider2D>(), GameObject.FindWithTag("Player2").GetComponent<Collider2D>());
        }
    }

    //powerups
    public void GLovesPU()
    {
        StartCoroutine(GlovesCo());
        _audio.PlayOneShot(soundPU);
    }
    public IEnumerator GlovesCo()
    {
        p1Gloves = GameObject.FindWithTag("P1Gloves");
        timeNeeded /= 2;
        p1Gloves.GetComponent<Image>().enabled = true; //p2Gloves.SetActive(true);
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p1HasPU = true;
        PlayersManager.p1HasGlovesPU = true;
        PlayersManager.p1PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        timeNeeded *= 2;
        p1Gloves.GetComponent<Image>().enabled = false; //p2Gloves.SetActive(false);
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p1HasPU = false;
        PlayersManager.p1HasGlovesPU = false;
        PlayersManager.p1PUTextShown = false;
    }
    public void BootsPU()
    {
        StartCoroutine(BootsCo());
        _audio.PlayOneShot(soundPU);

    }
    public IEnumerator BootsCo()
    {
        p1Boots = GameObject.FindWithTag("P1Boots");
        speed *= 2;
        p1Boots.GetComponent<Image>().enabled = true; //p2Boots.SetActive(true);
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p1HasPU = true;
        PlayersManager.p1HasBootsPU = true;
        PlayersManager.p1PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        speed /= 2;
        p1Boots.GetComponent<Image>().enabled = false;
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p1HasPU = false;
        PlayersManager.p1HasBootsPU = false;
        PlayersManager.p1PUTextShown = false;
    }
    public void ExtraSlotPU()
    {
        StartCoroutine(ExtraSlotCo());
        _audio.PlayOneShot(soundPU);
    }
    public IEnumerator ExtraSlotCo()
    {
        p1ES = GameObject.FindWithTag("P1ES");
        p1Slot = GameObject.FindWithTag("P1SBSlot");
        PlayersManager.p1MaximumSnowballsN++;
        p1ES.GetComponent<Image>().enabled = true; //p2Slot.SetActive(true);
        p1Slot.GetComponent<Image>().enabled = true;
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p1HasPU = true;
        PlayersManager.p1HasESPU = true;
        PlayersManager.p1PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        PlayersManager.p1MaximumSnowballsN--;
        p1ES.GetComponent<Image>().enabled = false;
        p1Slot.GetComponent<Image>().enabled = false;
        if (PlayersManager.p1SnowballsN > 2)
        {
            PlayersManager.p1SnowballsN = 2;
        }
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        PlayersManager.p1HasPU = false;
        PlayersManager.p1HasESPU = false;
        PlayersManager.p1PUTextShown = false;
    }
    public void ShieldPU()
    {
        StartCoroutine(ShieldCO());
        _audio.PlayOneShot(soundPU);
    }
    public IEnumerator ShieldCO()
    {
        p1Shield = GameObject.FindWithTag("P1Shield");
        p1Shield.GetComponent<Image>().enabled = true;
        showPUTimer = true;
        textPuGO.GetComponent<Text>().enabled = true;
        timerPU = powerUpTime;
        PlayersManager.p1HasPU = true;
        PlayersManager.p1HasShieldPU = true;
        PlayersManager.p1PUTextShown = true;
        yield return new WaitForSeconds(powerUpTime);
        p1Shield.GetComponent<Image>().enabled = false;
        showPUTimer = false;
        textPuGO.GetComponent<Text>().enabled = false;
        textPU.gameObject.SetActive(false);
        PlayersManager.p1HasPU = false;
        PlayersManager.p1HasShieldPU = false;
        PlayersManager.p1PUTextShown = false;
    }


    void Update()
    {
        anim.SetBool("grounded", p1Grounded);
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("P1Horizontal")));

        if (Input.GetKeyDown("a"))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            m_FacingRight = false;
        }

        if (Input.GetKeyDown("d"))
        {
            transform.localScale = new Vector3(1, 1, 1);
            m_FacingRight = true;
        }

        PLayer1Controls();
        anim.SetBool("shooting", p1shooting);
        anim.SetBool("picking sb", pickingSB);

        if(showPUTimer)
        {
            textPuGO = GameObject.FindWithTag("P1Text");
            timerPU -= Time.deltaTime;
            textPuGO.GetComponent<Text>().text = timerPU.ToString("0");
        }

    }

    void FixedUpdate()
    {
        SpeedControl();
    }
}
