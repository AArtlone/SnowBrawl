using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Base : MonoBehaviour {

    public static bool p2NearBase = false;
    public static bool p1NearP2Base = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player2")
        {
            p2NearBase = true;
        }
        if (col.gameObject.name == "Player1")
        {
            p1NearP2Base = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player2")
        {
            p2NearBase = false;
        }
        if (col.gameObject.name == "Player1")
        {
            p1NearP2Base = false;
        }
    }
}
