using TMPro;
using UnityEngine;

public class LorePopupUI : MonoBehaviour
{
    public static LorePopupUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    private void Awake()
    {
        Instance = this;

        panel.SetActive(false);
    }

    public void ShowLore(LoreData lore)
    {
        titleText.text = lore.title;
        descriptionText.text = lore.description;

        panel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void CloseLore()
    {
        panel.SetActive(false);

        Time.timeScale = 1f;
    }
}
