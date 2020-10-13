using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> platformToSpawnPUOn;

    [SerializeField] private List<GameObject> powerUpsPrefabs;

    [Space(10f)]
    [SerializeField] private int spawnDelay;
    [SerializeField] private int powerUpExistanceDuration;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager instance does not exist");
            return;
        }

        GameManager.onRoundStart += OnRoundStarted;
        GameManager.onPowerUpPickUp += OnPowerUpPickUp;
    }

    private void OnRoundStarted()
    {
        SpawnPowerUp();
    }

    private void OnPowerUpPickUp()
    {
        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        StartCoroutine(SpawnPowerUpsCo());
    }

    private IEnumerator SpawnPowerUpsCo()
    {
        yield return new WaitForSeconds(spawnDelay);

        if (GameManager.GameIsPaused)
            yield break;

        Transform platformToSpawnOn = GetRandomPlatform();

        Vector2 posToSpawnOn = new Vector2(
            platformToSpawnOn.position.x, 
            platformToSpawnOn.position.y + 2f);

        GameObject powerUp = Instantiate(GetRandomPrefab(), posToSpawnOn, Quaternion.identity);

        powerUp.transform.parent = platformToSpawnOn;

        yield return new WaitForSeconds(powerUpExistanceDuration);
        
        if (GameManager.GameIsPaused)
            yield break;

        if (powerUp == null)
            yield break;

        Destroy(powerUp);

        SpawnPowerUp();
    }

    private GameObject GetRandomPrefab()
    {
        int rnd = Random.Range(0, powerUpsPrefabs.Count);

        return powerUpsPrefabs[rnd];
    }

    private Transform GetRandomPlatform()
    {
        int rnd = Random.Range(0, platformToSpawnPUOn.Count);

        return platformToSpawnPUOn[rnd];
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
