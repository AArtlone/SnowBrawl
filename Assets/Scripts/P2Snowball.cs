using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Snowball : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    private Player2Controller p2;

    private void Start()
    {
        p2 = FindObjectOfType<Player2Controller>();
        if (p2.m_FacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!p2.m_FacingRight)
        {
            rb.velocity = transform.right * -speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            Destroy(gameObject);
            PlayersManager.p1Dead = true;
            PlayersManager.p1SnowballsN = 0;
        }
        if (col.gameObject.tag == "hitable")
        {
            Destroy(gameObject);
        }
    }

}
