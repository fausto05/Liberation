using TMPro;
using UnityEngine;

public class LoreLibraryUI : MonoBehaviour
{
    [SerializeField] private GameObject libraryPanel;

    [SerializeField] private GameObject pauseButtonsPanel;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    public void OpenLibrary()
    {
        pauseButtonsPanel.SetActive(false);
        libraryPanel.SetActive(true);
    }

    public void CloseLibrary()
    {
        libraryPanel.SetActive(false);
        pauseButtonsPanel.SetActive(true);
    }

    public void ShowLore(int id)
    {
        if (!LoreManager.Instance.IsUnlocked(id))
        {
            titleText.text = "?????";
            descriptionText.text = "Registro no encontrado.";
            return;
        }

        LoreData lore = LoreManager.Instance.GetLore(id);

        titleText.text = lore.title;
        descriptionText.text = lore.description;
    }
}
