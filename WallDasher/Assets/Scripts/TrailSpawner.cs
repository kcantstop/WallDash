using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private float spawnDistance = 0.5f;

    private Vector3 _lastSpawnPos;

    void Start()
    {
        _lastSpawnPos = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _lastSpawnPos) >= spawnDistance)
        {
            // Spawn where the player just WAS, not where they currently are -
            // avoids the trail overlapping the player's own collider the instant it appears
            Instantiate(trailPrefab, _lastSpawnPos, Quaternion.identity);
            _lastSpawnPos = transform.position;
        }
    }
}