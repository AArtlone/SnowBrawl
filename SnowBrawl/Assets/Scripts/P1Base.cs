using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Base : MonoBehaviour {

    public static bool p1NearBase = false;
    public static bool p2NearP1Base = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            p1NearBase = true;
        }
        if (col.gameObject.name == "Player2")
        {
            p2NearP1Base = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            p1NearBase = false;
        }
        if (col.gameObject.name == "Player2")
        {
            p2NearP1Base = false;
        }
    }
}
