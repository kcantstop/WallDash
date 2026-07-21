using System.Collections;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [SerializeField] private float boostedSpeed = 7.0f;
    [SerializeField] private float boostDuration = 3.0f;

    private bool _collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_collected) return;

        if (other.CompareTag("Player"))
        {
            GridMovement movement = other.GetComponent<GridMovement>();
            if (movement != null)
            {
                _collected = true;

                // Hide the pickup but keep the object alive so the coroutine can finish
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                SoundManager.Instance.PlayPickup();
                StartCoroutine(BoostRoutine(movement));
            }
        }
    }

    private IEnumerator BoostRoutine(GridMovement movement)
    {
        movement.CurrentSpeed = boostedSpeed;
        yield return new WaitForSeconds(boostDuration);
        movement.CurrentSpeed = movement.BaseSpeed;

        Destroy(gameObject);
    }
}
