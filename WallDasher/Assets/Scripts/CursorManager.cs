using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        bool inGameScene = SceneManager.GetActiveScene().name == "Main";
        bool playing = GameBehavior.Instance != null
                       && GameBehavior.Instance.CurrentState == GameBehavior.GameState.Playing;

        // Hide the cursor only during active gameplay; show it everywhere else
        if (inGameScene && playing)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}