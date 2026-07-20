using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool _isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;

        if (other.CompareTag("Trail") || other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        GameBehavior.Instance.PlayerDied(gameObject.name);
    }
}