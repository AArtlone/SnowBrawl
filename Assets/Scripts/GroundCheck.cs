using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private Player1Controller p1c;
    private Player2Controller p2c;

    void Start()
    {
        p1c = FindObjectOfType<Player1Controller>();
        p2c = FindObjectOfType<Player2Controller>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            col.gameObject.GetComponent<Player1Controller>().PlayHitSound();
        }
        if (col.gameObject.name == "Player2")
        {
            col.gameObject.GetComponent<Player2Controller>().PlayHitSound();
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            Player1Controller.p1Grounded = true;
        }
        if (col.gameObject.name == "Player2")
        {
            Player2Controller.p2Grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            Player1Controller.p1Grounded = false;
        }
        if (col.gameObject.name == "Player2")
        {
            Player2Controller.p2Grounded = false;
        }
    }

}
