using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        bool hasSave =
            data.missionIndex > 0 &&
            !data.gameCompleted;

        continueButton.SetActive(hasSave);
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();

        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Salir");
    }
}

