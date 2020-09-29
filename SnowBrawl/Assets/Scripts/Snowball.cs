using UnityEngine;

public class Snowball : MonoBehaviour 
{

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
