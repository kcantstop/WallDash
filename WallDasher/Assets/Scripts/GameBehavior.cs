using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    [SerializeField] private int startingLives = 3;

    private int _player1Lives;
    private int _player2Lives;

    private bool _roundOver = false;
    private HashSet<string> _deadThisFrame = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _player1Lives = startingLives;
            _player2Lives = startingLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied(string playerName)
    {
        if (_roundOver) return;
        _deadThisFrame.Add(playerName);
    }

    void LateUpdate()
    {
        if (_roundOver || _deadThisFrame.Count == 0) return;

        _roundOver = true;
        FreezeAll();

        if (_deadThisFrame.Contains("Player1")) _player1Lives--;
        if (_deadThisFrame.Contains("Player2")) _player2Lives--;

        StartCoroutine(EndRoundRoutine());
    }

    private IEnumerator EndRoundRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        if (_player1Lives <= 0 || _player2Lives <= 0)
        {
            // Game over - reset for next full game and return to menu
            ResetGame();
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            // Next round - reload game scene, countdown handles the rest
            ResetRound();
            SceneManager.LoadScene("Main");
        }
    }

    private void ResetRound()
    {
        _roundOver = false;
        _deadThisFrame.Clear();
    }

    private void ResetGame()
    {
        ResetRound();
        _player1Lives = startingLives;
        _player2Lives = startingLives;
    }

    public int GetLives(string playerName)
    {
        return playerName == "Player1" ? _player1Lives : _player2Lives;
    }

    private void FreezeAll()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<GridMovement>().enabled = false;
            player.GetComponent<TrailSpawner>().enabled = false;

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
        }
    }
}
