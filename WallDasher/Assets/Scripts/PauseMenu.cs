using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [SerializeField] private GameObject pausePanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void Show()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void Hide()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    
    public void OnResumePressed()
    {
        GameBehavior.Instance.ResumeGame();
    }

    
    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        GameBehavior.Instance.ResetGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    
    public void OnQuitToMenuPressed()
    {
        Time.timeScale = 1f;
        GameBehavior.Instance.ResetGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    
    public void OnQuitGamePressed()
    {
        Application.Quit();
    }
}
