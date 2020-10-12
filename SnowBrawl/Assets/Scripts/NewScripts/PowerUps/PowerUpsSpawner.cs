using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> platformToSpawnPUOn;

    [SerializeField] private List<GameObject> powerUpsPrefabs;

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

        GameObject powerUp = Instantiate(GetRandomPrefab(), GetRandomPosition(), Quaternion.identity);

        yield return new WaitForSeconds(powerUpExistanceDuration);

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

    private Vector2 GetRandomPosition()
    {
        int rnd = Random.Range(0, platformToSpawnPUOn.Count);

        var spawnPos = platformToSpawnPUOn[rnd].position;

        spawnPos.y += 2f;

        return spawnPos;
    }

    private GameManager GameManager { get { return GameManager.Instance; } }
}
