using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player1")
        {
            PlayersManager.p1Dead = true;
            
        }
        if (col.gameObject.name == "Player2")
        {
            PlayersManager.p2Dead = true;
        }
    }
}
