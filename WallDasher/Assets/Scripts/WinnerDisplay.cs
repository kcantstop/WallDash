using UnityEngine;
using TMPro;

public class WinnerDisplay : MonoBehaviour
{
    public static WinnerDisplay Instance;

    [SerializeField] private TMP_Text winnerText;

    void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        winnerText.text = message;
        winnerText.gameObject.SetActive(true);
    }
}
