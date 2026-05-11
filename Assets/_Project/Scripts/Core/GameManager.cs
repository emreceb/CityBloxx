using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, GameOver }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameplayScene")
            ChangeState(GameState.Playing);
        else
            ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game State: " + newState);
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
        if (TowerManager.Instance != null)
            TowerManager.Instance.ResetTower();
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
        GameOverUI ui = FindFirstObjectByType<GameOverUI>();
        if (ui != null) ui.ShowGameOver();

        AudioManager.Instance.PlayGameOver();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "GameplayScene")
            ChangeState(GameState.Playing);
        else if (scene.name == "MainMenu")
            ChangeState(GameState.MainMenu);
    }
}