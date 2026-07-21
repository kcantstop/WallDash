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
            GameObject segment = Instantiate(trailPrefab, _lastSpawnPos, Quaternion.identity);
            segment.GetComponent<TrailDelay>().SetOwner(gameObject.name);
            _lastSpawnPos = transform.position;
        }
    }
}