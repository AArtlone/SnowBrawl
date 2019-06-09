using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBallMaker : MonoBehaviour {
    
    public static bool p1NearSnowballBase = false;
    public static bool p2NearSnowballBase = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player1")
        {
            p1NearSnowballBase = true;
        }
        if (col.gameObject.name == "Player2")
        {
            p2NearSnowballBase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player1")
        {
            p1NearSnowballBase = false;
        }
        if (col.gameObject.name == "Player2")
        {
            p2NearSnowballBase = false;
        } 
    }
}
