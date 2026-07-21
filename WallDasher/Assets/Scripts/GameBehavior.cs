using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    public enum GameState { Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.Playing;

    [SerializeField] private int startingLives = 3;

    private int _player1Lives;
    private int _player2Lives;

    private bool _roundOver = false;
    public bool RoundOver => _roundOver;

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

    void Update()
    {
        // Only allows pausing during active play, and only in the game scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        if (SceneManager.GetActiveScene().name != "Main") return;

        CurrentState = GameState.Paused;
        Time.timeScale = 0f;

        if (PauseMenu.Instance != null)
        {
            PauseMenu.Instance.Show();
        }
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;

        if (PauseMenu.Instance != null)
        {
            PauseMenu.Instance.Hide();
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

        if (_player1Lives <= 0 && _player2Lives <= 0)
        {
            CurrentState = GameState.GameOver;
            SoundManager.Instance.PlayDraw();
            ShowRoundMessage("DRAW!");
        }
        else if (_player1Lives <= 0 || _player2Lives <= 0)
        {
            CurrentState = GameState.GameOver;
            string winner = _player1Lives <= 0 ? "Player 2" : "Player 1";
            SoundManager.Instance.PlayWinner();
            ShowRoundMessage(winner + " WINS!");
        }
        else if (_deadThisFrame.Count > 1)
        {
            SoundManager.Instance.PlayDraw();
            ShowRoundMessage("DRAW!");
        }

        StartCoroutine(EndRoundRoutine());
    }

    private IEnumerator EndRoundRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        if (_player1Lives <= 0 || _player2Lives <= 0)
        {
            ResetGame();
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            ResetRound();
            SceneManager.LoadScene("Main");
        }
    }

    private void ResetRound()
    {
        _roundOver = false;
        _deadThisFrame.Clear();
        CurrentState = GameState.Playing;
    }

    public void ResetGame()
    {
        ResetRound();
        _player1Lives = startingLives;
        _player2Lives = startingLives;
    }

    public int GetLives(string playerName)
    {
        return playerName == "Player1" ? _player1Lives : _player2Lives;
    }

    private void ShowRoundMessage(string message)
    {
        if (WinnerDisplay.Instance != null)
        {
            WinnerDisplay.Instance.ShowMessage(message);
        }
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
