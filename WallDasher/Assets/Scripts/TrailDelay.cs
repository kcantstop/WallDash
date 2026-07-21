using UnityEngine;

public class TrailDelay : MonoBehaviour
{
    [SerializeField] private float activationDelay = 0.1f;
    private Collider2D _collider;

    public string OwnerName { get; private set; }

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    void Start()
    {
        Invoke(nameof(ActivateCollider), activationDelay);
    }

    public void SetOwner(string ownerName)
    {
        OwnerName = ownerName;
    }

    private void ActivateCollider()
    {
        _collider.enabled = true;
    }
}