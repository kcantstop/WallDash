using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _roundOver = false;
    private HashSet<string> _deadThisFrame = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        { Instance = this;
        }
        else
        { Destroy(gameObject);
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

        if (_deadThisFrame.Count > 1)
        {
            Debug.Log("Draw - both players crashed at the same time!");
        }
        else
        {
            List<string> loserList = new List<string>(_deadThisFrame);
            string loser = loserList[0];
            string winner = loser == "Player1" ? "Player2" : "Player1";
            Debug.Log(winner + " wins the round!");
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

    void Update()
    {
        if (_roundOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
