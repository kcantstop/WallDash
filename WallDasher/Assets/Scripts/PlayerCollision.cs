using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool _isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead || GameBehavior.Instance.RoundOver) return;

        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayHeadOn();
            Die();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Trail"))
        {
            SoundManager.Instance.PlayCrash();
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        GetComponent<DeathFlicker>().StartFlicker();
        GameBehavior.Instance.PlayerDied(gameObject.name);
    }
}