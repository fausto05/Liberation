using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;

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

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        masterVolumeSlider.SetValueWithoutNotify(savedVolume);
        SetMasterVolume(savedVolume);
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

        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        settingsPanel.SetActive(false);
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

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
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

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        float volumeInDecibels = Mathf.Log10(volume) * 20f;

        bool changed = audioMixer.SetFloat(
            "MasterVolume",
            volumeInDecibels
        );

        Debug.Log(
            $"Slider: {volume} | Decibeles: {volumeInDecibels} | Cambiado: {changed}"
        );

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
