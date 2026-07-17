using UnityEngine;

public class TrailDelay : MonoBehaviour
{
    [SerializeField] private float activationDelay = 0.1f;
    private Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    void Start()
    {
        Invoke(nameof(ActivateCollider), activationDelay);
    }

    private void ActivateCollider()
    {
        _collider.enabled = true;
    }
}