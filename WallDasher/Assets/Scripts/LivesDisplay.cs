using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] player1Hearts;
    [SerializeField] private GameObject[] player2Hearts;

    void Start()
    {
        UpdateHearts(player1Hearts, GameBehavior.Instance.GetLives("Player1"));
        UpdateHearts(player2Hearts, GameBehavior.Instance.GetLives("Player2"));
    }

    private void UpdateHearts(GameObject[] hearts, int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }
}