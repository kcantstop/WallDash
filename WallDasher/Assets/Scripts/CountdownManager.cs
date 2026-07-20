using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float stepDuration = 1.0f;

    void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        SetPlayersActive(false);

        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(stepDuration);

        countdownText.text = "2";
        yield return new WaitForSeconds(stepDuration);

        countdownText.text = "1";
        yield return new WaitForSeconds(stepDuration);

        countdownText.text = "GO";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);
        SetPlayersActive(true);
    }

    private void SetPlayersActive(bool active)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<GridMovement>().enabled = active;
            player.GetComponent<TrailSpawner>().enabled = active;

            if (!active)
            {
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }
}
