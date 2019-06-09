using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsPowerUp : MonoBehaviour {

    private bool _isPickedUp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player1" && !PlayersManager.p1HasPU)
        {
            col.GetComponent<Player1Controller>().BootsPU();
            FindObjectOfType<PUSpawner>().StartPUSpawnCo();
            Destroy(gameObject);
            //play sound
        }
        if (col.gameObject.name == "Player2" && !PlayersManager.p2HasPU)
        {
            col.GetComponent<Player2Controller>().BootsPU();
            FindObjectOfType<PUSpawner>().StartPUSpawnCo();
            Destroy(gameObject);
        }
    }
    public IEnumerator DespawnCo()
    {
        yield return new WaitForSeconds(10f);
        if (!_isPickedUp)
        {
            Destroy(gameObject);
            FindObjectOfType<PUSpawner>().StartPUSpawnCo();
        }
    }

    private void Start()
    {
        StartCoroutine(DespawnCo());
    }
}
