using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject victoryPanel;

    private bool isPaused;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        Time.timeScale = 1f;
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void ShowVictory()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void RetryLevel()
    {
        SaveData data = SaveSystem.Load();

        data.playerHealth = 100;

        SaveSystem.Save(data);

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(!missionPanel.activeSelf);
    }

    private void OnEnable()
    {
        GameEvents.OnBossKilled += ShowVictory;
    }

    private void OnDisable()
    {
        GameEvents.OnBossKilled -= ShowVictory;
    }
}
