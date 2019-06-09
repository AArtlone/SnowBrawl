using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Snowball : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;

    private Player1Controller p1;
    

    private void Start()
    {
        p1 = FindObjectOfType<Player1Controller>();
        if(p1.m_FacingRight)
        {
            rb.velocity = transform.right * speed;
        } else if (!p1.m_FacingRight)
        {
            rb.velocity = transform.right * -speed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player2")
        {
            Destroy(gameObject);
            PlayersManager.p2Dead = true;
            PlayersManager.p2SnowballsN = 0;
        }
        if (col.gameObject.tag == "hitable")
        {
            Destroy(gameObject);
        }
    }
}
