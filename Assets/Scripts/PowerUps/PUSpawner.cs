using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUSpawner : MonoBehaviour {

    public GameObject[] allPlatforms;
    public GameObject[] allPU;

    private Vector3 offSet = new Vector3(0, 3f, 0);

    private void Start()
    {
        StartCoroutine(SpawnPU());
    }

    public void StartPUSpawnCo()
    {
        StartCoroutine(SpawnPU());
    }

    private IEnumerator SpawnPU()
    {
        yield return new WaitForSeconds(10f);
        int posToUse = Random.Range(0, allPlatforms.Length);
        int puToUse = Random.Range(0, allPU.Length);
        while((PlayersManager.p1MaximumSnowballsN > 2 || PlayersManager.p2MaximumSnowballsN > 2) && allPU[puToUse].gameObject.name == "snowball")
        {
            puToUse = Random.Range(0, allPU.Length);
        }
        Instantiate(allPU[puToUse], allPlatforms[posToUse].transform.position + offSet, Quaternion.identity);
    }

}
