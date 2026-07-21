using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnDelay = 4.0f;

    [Header("Spawn Area (arena bounds)")]
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(x, y, 0f);

        Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
    }
}